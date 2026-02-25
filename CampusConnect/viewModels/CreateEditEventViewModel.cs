using CampusConnect.Models;
using System.ComponentModel.DataAnnotations;

namespace CampusConnect.viewModels
{
    public class CreateEditEventViewModel
    {
        
    // Event fields
    public string? EventId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Category { get; set; }

        // Existing location selection
        public string? SelectedLocationId { get; set; }
        public List<Location> Locations { get; set; } = new();
        // New location fields
        public bool CreateNewLocation { get; set; }
        public string? NewLocationName { get; set; }
        public string? NewLocationDescription { get; set; }
    }
}

