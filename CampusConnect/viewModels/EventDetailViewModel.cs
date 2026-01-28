using CampusConnect.Models;

namespace CampusConnect.viewModels
{
    public class EventDetailViewModel
    {
        public Event Event { get; set; }
        public string CreatorDisplayName { get; set; }
        public bool CanRSVP { get; set; }
        public bool CanEdit { get; set; }
        public bool IsRSVPed { get; set; }
    }

}
