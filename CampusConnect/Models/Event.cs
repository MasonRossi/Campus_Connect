using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Models
{
    public class Event
    {
        [Key]
        public string EventId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string CreatedBy { get; set; } // Organizer's UserId

        [Required]
        public string Category { get; set; }

        public int RSVPCount { get; set; }
    }
}
