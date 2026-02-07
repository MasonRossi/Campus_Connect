using CampusConnect.Data.Repositories;
using CampusConnect.Models;
using CampusConnect.viewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CampusConnect.Controllers
{
    [Authorize]
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

        // login (anyone)
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            SetHeaderButtons();
            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetHeaderButtons();
                return View(model);
            }

            var user = await _unitOfWork.Users
                .GetByDisplayNameAsync(model.DisplayName);

            if (user != null)
            {
                var result = _passwordHasher.VerifyHashedPassword(
                    user,
                    user.Password,
                    model.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    await SignInUser(user);
                    return RedirectToAction("Home");
                }
            }

            model.ErrorMessage = "Username or password not found.";
            SetHeaderButtons();
            return View(model);
        }

        // Register (anyone)
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            SetHeaderButtons();
            return View(new RegisterViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine(
                            $"Field: {state.Key} | Error: {error.ErrorMessage}");
                    }
                }

                SetHeaderButtons();
                return View(model);
            }

            if (await _unitOfWork.Users
                .ExistsByDisplayNameAsync(model.DisplayName))
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

            newUser.Password = _passwordHasher
                .HashPassword(newUser, model.Password);

            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.CompleteAsync();

            await SignInUser(newUser);
            return RedirectToAction("Home");
        }

        // logout (logged in)
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Home");
        }

        // home/start page (anyone)
        [AllowAnonymous]
        public IActionResult Home()
        {
            SetHeaderButtons();
            return View();
        }

        // list of events (anyone)
        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            var events = await _unitOfWork.Events
                .GetAllWithLocationAsync();

            SetHeaderButtons();
            return View(events.OrderBy(e => e.Date));
        }

        // view user profile (logged in)
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login");

            var user = await _unitOfWork.Users
                .GetByIdWithRSVPsAndCreatedEventsAsync(userId);

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
                    .Select(r => r.Event)
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

        // view event details (anyone)
        [AllowAnonymous]
        public async Task<IActionResult> Detail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("List");

            var ev = await _unitOfWork.Events
                .GetByIdWithAllRelationsAsync(id);

            if (ev == null)
                return RedirectToAction("List");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);

            var model = new EventDetailViewModel
            {
                Event = ev,
                CreatorDisplayName = ev.CreatedBy?.DisplayName ?? "Unknown",
                CanEdit = userId != null && ev.CreatedById == userId,
                CanRSVP = userId != null &&
                          role == "Student" &&
                          ev.CreatedById != userId,
                IsRSVPed = userId != null &&
                           ev.RSVPs.Any(r => r.UserId == userId)
            };

            SetHeaderButtons();
            return View(model);
        }

        // create a new event (organizers)
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> Create(string id = null)
        {
            SetHeaderButtons();

            if (string.IsNullOrEmpty(id))
                return View(new Event());

            var ev = await _unitOfWork.Events
                .GetByIdWithLocationAsync(id);

            if (ev == null)
                return RedirectToAction("List");

            if (ev.CreatedById != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return Forbid();

            return View(ev);
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> SaveEvent(Event model)
        {
            if (!ModelState.IsValid)
                return View("Create", model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(model.EventId))
            {
                model.EventId = Guid.NewGuid().ToString();
                model.CreatedById = userId;
                await _unitOfWork.Events.AddAsync(model);
            }
            else
            {
                var ev = await _unitOfWork.Events
                    .GetByIdWithLocationAsync(model.EventId);

                if (ev == null || ev.CreatedById != userId)
                    return Forbid();

                ev.Title = model.Title;
                ev.Description = model.Description;
                ev.Date = model.Date;
                ev.Category = model.Category;
                ev.LocationId = model.LocationId;
            }

            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Detail",
                new { id = model.EventId });
        }

        // RSVP to event (Student)
        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> RSVP(string eventId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ev = await _unitOfWork.Events
                .GetByIdWithRSVPsAsync(eventId);

            if (ev == null)
                return RedirectToAction("List");

            var existing = ev.RSVPs
                .FirstOrDefault(r => r.UserId == userId);

            if (existing != null)
            {
                _unitOfWork.RSVPs.Remove(existing);
                TempData["RSVPMessage"] = "RSVP canceled.";
            }
            else
            {
                await _unitOfWork.RSVPs.AddAsync(new RSVP
                {
                    RSVPId = Guid.NewGuid().ToString(),
                    EventId = eventId,
                    UserId = userId
                });

                TempData["RSVPMessage"] = "RSVP successful.";
            }

            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Detail",
                new { id = eventId });
        }

        // Delete event (organizers)
        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> DeleteEvent(string eventId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var ev = await _unitOfWork.Events
                .GetByIdWithRSVPsAsync(eventId);

            if (ev == null || ev.CreatedById != userId)
                return Forbid();

            _unitOfWork.RSVPs.RemoveRange(ev.RSVPs);
            _unitOfWork.Events.Remove(ev);

            await _unitOfWork.CompleteAsync();

            return RedirectToAction("List");
        }

        // Access denied page
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            SetHeaderButtons();
            return View();
        }

        // Private Helpers
        // sign in users
        private async Task SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }

        // set buttons on the header
        private void SetHeaderButtons()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var buttons = new List<dynamic>();

            if (role == "Organizer")
            {
                buttons.AddRange(new[]
                {
                    new { Text = "Home", Controller = "Home", Action = "Home" },
                    new { Text = "Create Event", Controller = "Home", Action = "Create" },
                    new { Text = "Events", Controller = "Home", Action = "List" },
                    new { Text = "Profile", Controller = "Home", Action = "Profile" },
                    new { Text = "Logout", Controller = "Home", Action = "Logout" }
                });
            }
            else if (role == "Student")
            {
                buttons.AddRange(new[]
                {
                    new { Text = "Home", Controller = "Home", Action = "Home" },
                    new { Text = "Events", Controller = "Home", Action = "List" },
                    new { Text = "Profile", Controller = "Home", Action = "Profile" },
                    new { Text = "Logout", Controller = "Home", Action = "Logout" }
                });
            }
            else
            {
                buttons.AddRange(new[]
                {
                    new { Text = "Home", Controller = "Home", Action = "Home" },
                    new { Text = "Events", Controller = "Home", Action = "List" },
                    new { Text = "Login", Controller = "Home", Action = "Login"},
                    new { Text = "Register", Controller = "Home" , Action = "Register" }
                });
            }

            ViewBag.HeaderButtons = buttons;
        }
    }
}
