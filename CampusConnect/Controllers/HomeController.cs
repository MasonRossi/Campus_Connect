using CampusConnect.Data.Repositories;
using CampusConnect.Models;
using CampusConnect.viewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CampusConnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _passwordHasher = new PasswordHasher<User>();
        }

        // Login
        [HttpGet]
        public IActionResult Login()
        {
            SetHeaderButtons();
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _unitOfWork.Users.GetByDisplayNameAsync(model.DisplayName);
            if (user != null)
            {
                var result = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);
                if (result == PasswordVerificationResult.Success)
                {
                    SetUserSession(user);
                    return RedirectToAction("Home");
                }
            }

            model.ErrorMessage = "Username or password not found.";
            SetHeaderButtons();
            return View(model);
        }

        // Register
        [HttpGet]
        public IActionResult Register()
        {
            SetHeaderButtons();
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.DisplayName) ||
                string.IsNullOrWhiteSpace(model.Password) ||
                string.IsNullOrWhiteSpace(model.Role))
            {
                model.ErrorMessage = "Please fill in all required fields.";
                SetHeaderButtons();
                return View(model);
            }

            if (await _unitOfWork.Users.ExistsByDisplayNameAsync(model.DisplayName))
            {
                model.ErrorMessage = "Username already taken.";
                SetHeaderButtons();
                return View(model);
            }

            var newUser = new User
            {
                UserId = Guid.NewGuid().ToString(),
                DisplayName = model.DisplayName,
                Email = model.Email,
                Role = model.Role
            };

            newUser.Password = _passwordHasher.HashPassword(newUser, model.Password);

            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.CompleteAsync();

            SetUserSession(newUser);
            return RedirectToAction("Home");
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Home");
        }

        // Home
        public IActionResult Home()
        {
            SetHeaderButtons();
            return View();
        }

        // Event List
        public async Task<IActionResult> List()
        {
            var events = await _unitOfWork.Events.GetAllWithLocationAsync();
            SetHeaderButtons();
            return View(events.OrderBy(e => e.Date));
        }
        // access denied (temp)
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
        // Profile
        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login");

            var user = await _unitOfWork.Users.GetByIdWithRSVPsAndCreatedEventsAsync(userId);
            if (user == null) return RedirectToAction("Login");

            var model = new ProfileViewModel
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Role = user.Role
            };

            if (user.Role == "Student")
            {
                var events = new List<Event>();
                foreach (var rsvp in user.RSVPs)
                {
                    var ev = await _unitOfWork.Events.GetByIdWithLocationAsync(rsvp.EventId);
                    if (ev != null) events.Add(ev);
                }
                model.RSVPedEvents = events.OrderBy(e => e.Date).ToList();
            }
            else if (user.Role == "Organizer")
            {
                model.CreatedEvents = user.CreatedEvents.OrderBy(e => e.Date).ToList();
            }

            SetHeaderButtons();
            return View(model);
        }

        // Event Details
        public async Task<IActionResult> Detail(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("List");

            var ev = await _unitOfWork.Events.GetByIdWithAllRelationsAsync(id);
            if (ev == null) return RedirectToAction("List");

            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Role");

            var model = new EventDetailViewModel
            {
                Event = ev,
                CreatorDisplayName = ev.CreatedBy?.DisplayName ?? "Unknown",
                CanEdit = !string.IsNullOrEmpty(userId) && ev.CreatedById == userId,
                CanRSVP = !string.IsNullOrEmpty(userId) && role == "Student" && ev.CreatedById != userId,
                IsRSVPed = !string.IsNullOrEmpty(userId) && ev.RSVPs.Any(r => r.UserId == userId)
            };

            SetHeaderButtons();
            return View(model);
        }

        // Create/Edit Event
        public async Task<IActionResult> Create(string id = null)
        {
            SetHeaderButtons();
            if (string.IsNullOrEmpty(id)) return View(new Event());

            var ev = await _unitOfWork.Events.GetByIdWithLocationAsync(id);
            if (ev == null) return RedirectToAction("List");

            return View(ev);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEvent(Event model)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Role");

            if (role != "Organizer" || string.IsNullOrEmpty(userId))
                return RedirectToAction("List");

            if (string.IsNullOrWhiteSpace(model.Title) ||
                string.IsNullOrWhiteSpace(model.Description) ||
                model.Date == default ||
                string.IsNullOrEmpty(model.Category))
            {
                ViewBag.ErrorMessage = "Please fill in all required fields.";
                return View("Create", model);
            }

            if (string.IsNullOrEmpty(model.EventId))
            {
                model.EventId = Guid.NewGuid().ToString();
                model.CreatedById = userId;
                await _unitOfWork.Events.AddAsync(model);
            }
            else
            {
                var ev = await _unitOfWork.Events.GetByIdWithLocationAsync(model.EventId);
                if (ev == null) return RedirectToAction("List");

                ev.Title = model.Title;
                ev.Description = model.Description;
                ev.Date = model.Date;
                ev.Category = model.Category;
                ev.LocationId = model.LocationId;
            }

            await _unitOfWork.CompleteAsync();
            return RedirectToAction("Detail", new { id = model.EventId });
        }

        // RSVP/Delete Event
        [HttpPost]
        public async Task<IActionResult> RSVP(string EventId)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(userId) || role != "Student")
            {
                TempData["RSVPMessage"] = "You must be a student to RSVP.";
                return RedirectToAction("Detail", new { id = EventId });
            }

            var ev = await _unitOfWork.Events.GetByIdWithRSVPsAsync(EventId);
            if (ev == null) return RedirectToAction("List");

            var existingRSVP = ev.RSVPs.FirstOrDefault(r => r.UserId == userId);
            if (existingRSVP != null)
            {
                _unitOfWork.RSVPs.Remove(existingRSVP);
                TempData["RSVPMessage"] = "Your RSVP has been canceled.";
            }
            else
            {
                var newRSVP = new RSVP
                {
                    RSVPId = Guid.NewGuid().ToString(),
                    EventId = EventId,
                    UserId = userId
                };
                await _unitOfWork.RSVPs.AddAsync(newRSVP);
                TempData["RSVPMessage"] = "You have RSVPed to this event.";
            }

            await _unitOfWork.CompleteAsync();
            return RedirectToAction("Detail", new { id = EventId });
        }

        // Delete Event
        [HttpPost]
        public async Task<IActionResult> DeleteEvent(string EventId)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(EventId) || string.IsNullOrEmpty(userId) || role != "Organizer")
                return RedirectToAction("List");

            var ev = await _unitOfWork.Events.GetByIdWithRSVPsAsync(EventId);
            if (ev == null || ev.CreatedById != userId)
                return RedirectToAction("List");

            _unitOfWork.RSVPs.RemoveRange(ev.RSVPs);
            _unitOfWork.Events.Remove(ev);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("List");
        }

        // Private Helpers
        private void SetUserSession(User user)
        {
            HttpContext.Session.SetString("UserId", user.UserId);
            HttpContext.Session.SetString("DisplayName", user.DisplayName);
            HttpContext.Session.SetString("Role", user.Role);
        }

        private void SetHeaderButtons()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Role");
            var buttons = new List<dynamic>();

            if (!string.IsNullOrEmpty(userId) && role == "Organizer")
            {
                buttons.AddRange(new[]
                {
                    new { Text = "Home", Url = "/Home" },
                    new { Text = "Create Event", Url = "/Home/Create" },
                    new { Text = "Events", Url = "/Home/List" },
                    new { Text = "Profile", Url = "/Home/Profile" },
                    new { Text = "Logout", Url = "/Home/Logout" }
                });
            }
            else if (!string.IsNullOrEmpty(userId) && role == "Student")
            {
                buttons.AddRange(new[]
                {
                    new { Text = "Home", Url = "/Home" },
                    new { Text = "Events", Url = "/Home/List" },
                    new { Text = "Profile", Url = "/Home/Profile" },
                    new { Text = "Logout", Url = "/Home/Logout" }
                });
            }
            else
            {
                buttons.AddRange(new[]
                {
                    new { Text = "Home", Url = "/Home" },
                    new { Text = "Events", Url = "/Home/List" },
                    new { Text = "Login", Url = "/Home/Login" },
                    new { Text = "Register", Url = "/Home/Register" }
                });
            }

            ViewBag.HeaderButtons = buttons;
        }
    }
}
