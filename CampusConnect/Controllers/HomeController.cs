using CampusConnect.Data;
using CampusConnect.Models;
using CampusConnect.viewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CampusConnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        public IActionResult TestConnection()
        {
            try
            {
                var count = _context.Users.Count();
                return Content($"Connected! Users count: {count}");
            }
            catch (Exception ex)
            {
                return Content($"Failed: {ex.Message}");
            }
        }


        // ------------------------------
        // LOGIN
        // ------------------------------
        [HttpGet]
        public IActionResult Login()
        {
            SetHeaderButtons();
            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.DisplayName == model.DisplayName);
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

        // ------------------------------
        // REGISTER
        // ------------------------------
        [HttpGet]
        public IActionResult Register()
        {
            SetHeaderButtons();
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.DisplayName) ||
                string.IsNullOrWhiteSpace(model.Password) ||
                string.IsNullOrWhiteSpace(model.Role))
            {
                model.ErrorMessage = "Please fill in all required fields.";
                SetHeaderButtons();
                return View(model);
            }

            if (_context.Users.Any(u => u.DisplayName == model.DisplayName))
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

            // Hash the password
            newUser.Password = _passwordHasher.HashPassword(newUser, model.Password);

            _context.Users.Add(newUser);
            _context.SaveChanges();

            SetUserSession(newUser);
            return RedirectToAction("Home");
        }

        // ------------------------------
        // LOGOUT
        // ------------------------------
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Home");
        }

        // ------------------------------
        // HOME
        // ------------------------------
        public IActionResult Home()
        {
            SetHeaderButtons();
            return View();
        }

        // ------------------------------
        // EVENT LIST
        // ------------------------------
        public async Task<IActionResult> List()
        {
            var events = await _context.Events
                .Include(e => e.Location)
                .OrderBy(e => e.Date)
                .ToListAsync();

            SetHeaderButtons();
            return View(events);
        }

        // ------------------------------
        // PROFILE
        // ------------------------------
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login");

            var user = _context.Users
                .Include(u => u.RSVPs)
                .Include(u => u.CreatedEvents)
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
                return RedirectToAction("Login");

            var model = new ProfileViewModel
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Role = user.Role
            };

            if (user.Role == "Student")
            {
                model.RSVPedEvents = user.RSVPs
                    .Select(r => _context.Events.Include(e => e.Location)
                                                .FirstOrDefault(e => e.EventId == r.EventId))
                    .Where(e => e != null)
                    .OrderBy(e => e.Date)
                    .ToList();
            }
            else if (user.Role == "Organizer")
            {
                model.CreatedEvents = user.CreatedEvents
                    .OrderBy(e => e.Date)
                    .ToList();
            }

            SetHeaderButtons();
            return View(model);
        }

        // ------------------------------
        // EVENT DETAILS
        // ------------------------------
        public IActionResult Detail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("List");

            var ev = _context.Events
                .Include(e => e.Location)
                .Include(e => e.CreatedBy)
                .Include(e => e.RSVPs)
                .FirstOrDefault(e => e.EventId == id);

            if (ev == null)
                return RedirectToAction("List");

            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Role");

            bool canEdit = !string.IsNullOrEmpty(userId) && ev.CreatedById == userId;
            bool canRSVP = !canEdit && !string.IsNullOrEmpty(userId) && role == "Student";
            bool isRSVPed = !string.IsNullOrEmpty(userId) && ev.RSVPs.Any(r => r.UserId == userId);

            var model = new EventDetailViewModel
            {
                Event = ev,
                CreatorDisplayName = ev.CreatedBy?.DisplayName ?? "Unknown",
                CanEdit = canEdit,
                CanRSVP = canRSVP,
                IsRSVPed = isRSVPed
            };

            SetHeaderButtons();
            return View(model);
        }

        // ------------------------------
        // CREATE/EDIT EVENT
        // ------------------------------
        public IActionResult Create(string id = null)
        {
            SetHeaderButtons();
            if (string.IsNullOrEmpty(id))
                return View(new Event());

            var ev = _context.Events.Include(e => e.Location)
                                    .FirstOrDefault(e => e.EventId == id);
            if (ev == null) return RedirectToAction("List");

            return View(ev);
        }

        [HttpPost]
        public IActionResult SaveEvent(Event model)
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
                // New Event
                model.EventId = Guid.NewGuid().ToString();
                model.CreatedById = userId;
                _context.Events.Add(model);
            }
            else
            {
                // Edit existing event
                var ev = _context.Events.Include(e => e.Location)
                                        .FirstOrDefault(e => e.EventId == model.EventId);
                if (ev == null) return RedirectToAction("List");

                ev.Title = model.Title;
                ev.Description = model.Description;
                ev.Date = model.Date;
                ev.Category = model.Category;
                ev.LocationId = model.LocationId;
            }

            _context.SaveChanges();
            return RedirectToAction("Detail", new { id = model.EventId });
        }

        // ------------------------------
        // RSVP / CANCEL RSVP
        // ------------------------------
        [HttpPost]
        public IActionResult RSVP(string EventId)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(userId) || role != "Student")
            {
                TempData["RSVPMessage"] = "You must be a student to RSVP.";
                return RedirectToAction("Detail", new { id = EventId });
            }

            var ev = _context.Events.Include(e => e.RSVPs)
                                    .FirstOrDefault(e => e.EventId == EventId);
            if (ev == null) return RedirectToAction("List");

            var existingRSVP = ev.RSVPs.FirstOrDefault(r => r.UserId == userId);

            if (existingRSVP != null)
            {
                _context.RSVPs.Remove(existingRSVP);
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
                _context.RSVPs.Add(newRSVP);
                TempData["RSVPMessage"] = "You have RSVPed to this event.";
            }

            _context.SaveChanges();
            return RedirectToAction("Detail", new { id = EventId });
        }

        // ------------------------------
        // DELETE EVENT
        // ------------------------------
        [HttpPost]
        public IActionResult DeleteEvent(string EventId)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(EventId) || string.IsNullOrEmpty(userId) || role != "Organizer")
                return RedirectToAction("List");

            var ev = _context.Events.Include(e => e.RSVPs)
                                    .FirstOrDefault(e => e.EventId == EventId);

            if (ev == null || ev.CreatedById != userId)
                return RedirectToAction("List");

            _context.RSVPs.RemoveRange(ev.RSVPs);
            _context.Events.Remove(ev);
            _context.SaveChanges();

            return RedirectToAction("List");
        }

        // ------------------------------
        // PRIVATE HELPERS
        // ------------------------------
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
