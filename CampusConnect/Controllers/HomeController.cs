using CampusConnect.Data;
using CampusConnect.Models;
using CampusConnect.viewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CampusConnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
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
            var user = _context.Users
                .FirstOrDefault(u => u.DisplayName == model.DisplayName && u.Password == model.Password);

            if (user != null)
            {
                SetUserSession(user);
                return RedirectToAction("Home");
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
                return View(model);
            }

            if (_context.Users.Any(u => u.DisplayName == model.DisplayName))
            {
                model.ErrorMessage = "Username already taken.";
                return View(model);
            }

            var newUser = new User
            {
                UserId = Guid.NewGuid().ToString(),
                DisplayName = model.DisplayName,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role
            };

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

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
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
                // Get RSVPed events
                model.RSVPedEvents = _context.RSVPs
                    .Where(r => r.UserId == userId)
                    .Join(_context.Events,
                          r => r.EventId,
                          e => e.EventId,
                          (r, e) => e)
                    .OrderBy(e => e.Date)
                    .ToList();
            }
            else if (user.Role == "Organizer")
            {
                // Get events created by the organizer
                model.CreatedEvents = _context.Events
                    .Where(e => e.CreatedBy == userId)
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
                return RedirectToAction("List"); // No event ID provided

            var ev = _context.Events.FirstOrDefault(e => e.EventId == id);
            if (ev == null)
                return RedirectToAction("List"); // Event not found

            var creator = _context.Users.FirstOrDefault(u => u.UserId == ev.CreatedBy);
            string creatorDisplayName = creator != null ? creator.DisplayName : "Unknown";

            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Role");

            bool canEdit = !string.IsNullOrEmpty(userId) && ev.CreatedBy == userId;
            bool canRSVP = !canEdit && !string.IsNullOrEmpty(userId) && role == "Student";

            bool isRSVPed = false;
            if (!string.IsNullOrEmpty(userId))
            {
                isRSVPed = _context.RSVPs.Any(r => r.EventId == id && r.UserId == userId);
            }

            var model = new EventDetailViewModel
            {
                Event = ev,
                CreatorDisplayName = creatorDisplayName,
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
            else
            {
                var ev = _context.Events.FirstOrDefault(e => e.EventId == id);
                if (ev == null) return RedirectToAction("List");
                return View(ev);
            }
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
                string.IsNullOrWhiteSpace(model.Location) ||
                string.IsNullOrWhiteSpace(model.Category) ||
                model.Date == default)
            {
                ViewBag.ErrorMessage = "Please fill in all fields.";
                return View("Create", model);
            }

            if (string.IsNullOrEmpty(model.EventId))
            {
                // New Event
                model.EventId = Guid.NewGuid().ToString();
                model.CreatedBy = userId;
                model.RSVPCount = 0;
                _context.Events.Add(model);
            }
            else
            {
                // Edit existing event
                var ev = _context.Events.FirstOrDefault(e => e.EventId == model.EventId);
                if (ev == null) return RedirectToAction("List");

                ev.Title = model.Title;
                ev.Description = model.Description;
                ev.Location = model.Location;
                ev.Date = model.Date;
                ev.Category = model.Category;
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

            var existingRSVP = _context.RSVPs.FirstOrDefault(r => r.UserId == userId && r.EventId == EventId);
            var ev = _context.Events.FirstOrDefault(e => e.EventId == EventId);

            if (ev == null)
            {
                TempData["RSVPMessage"] = "Event not found.";
                return RedirectToAction("List");
            }

            if (existingRSVP != null)
            {
                // Cancel RSVP
                _context.RSVPs.Remove(existingRSVP);
                if (ev.RSVPCount > 0) ev.RSVPCount--;
                TempData["RSVPMessage"] = "Your RSVP has been canceled.";
            }
            else
            {
                // Add RSVP
                var newRSVP = new RSVP
                {
                    RSVPId = Guid.NewGuid().ToString(),
                    UserId = userId,
                    EventId = EventId
                };
                _context.RSVPs.Add(newRSVP);
                ev.RSVPCount++;
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

            var ev = _context.Events.FirstOrDefault(e => e.EventId == EventId);

            if (ev == null || ev.CreatedBy != userId)
                return RedirectToAction("List"); // Can't delete events not created by this user

            // Remove associated RSVPs first
            var rsvps = _context.RSVPs.Where(r => r.EventId == EventId).ToList();
            _context.RSVPs.RemoveRange(rsvps);

            // Remove the event
            _context.Events.Remove(ev);
            _context.SaveChanges();

            return RedirectToAction("List"); // Redirect back to events list
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
                    new { Text = "About", Url = "#" },
                    new { Text = "Contact", Url = "#" },
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
                    new { Text = "About", Url = "#" },
                    new { Text = "Contact", Url = "#" },
                    new { Text = "Logout", Url = "/Home/Logout" }
                });
            }
            else
            {
                buttons.AddRange(new[]
                {
                    new { Text = "Home", Url = "/Home" },
                    new { Text = "Events", Url = "/Home/List" },
                    new { Text = "About", Url = "#" },
                    new { Text = "Contact", Url = "#" },
                    new { Text = "Login", Url = "/Home/Login" },
                    new { Text = "Register", Url = "/Home/Register" }
                });
            }

            ViewBag.HeaderButtons = buttons;
        }
    }
}
