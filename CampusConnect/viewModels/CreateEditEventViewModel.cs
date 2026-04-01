using CampusConnect.Models;
using System.ComponentModel.DataAnnotations;

namespace CampusConnect.viewModels
{
    public class CreateEditEventViewModel
    {
        
    // Event fields
    public string? EventId { get; set; }

        [Required(ErrorMessage = "Must enter a Title")]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Must enter a Date")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Must enter a Category")]
        public string Category { get; set; }

        // Existing location selection
        [Required(ErrorMessage = "Must enter a location")]
        public string SelectedLocationId { get; set; }
        public List<Location> Locations { get; set; } = new();
        // New location fields
        public bool CreateNewLocation { get; set; }
        [Required(ErrorMessage = "Must enter a name for you new location")]
        public string NewLocationName { get; set; }
        public string? NewLocationDescription { get; set; } = "";
    
    
    }
}

