using CampusConnect.Models;

namespace CampusConnect.viewModels
{
    public class EventListViewModel
    {
        public List<Event> Events { get; set; } = new();
        public List<Location> Locations { get; set; } = new();
    }
}
