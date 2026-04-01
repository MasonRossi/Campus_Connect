using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Models
{
    public class Location
    {
        [Key]
        public string LocationId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; } = "";

        // Optional: navigation to events at this location
        public ICollection<Event> Events { get; set; }
    }
}
