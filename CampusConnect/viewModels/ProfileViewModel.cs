using CampusConnect.Models;
using System.Collections.Generic;

namespace CampusConnect.viewModels
{
    public class ProfileViewModel
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        // Events the student has RSVPed to
        public List<Event> RSVPedEvents { get; set; } = new List<Event>();

        // Events the organizer has created
        public List<Event> CreatedEvents { get; set; } = new List<Event>();
    }
}
