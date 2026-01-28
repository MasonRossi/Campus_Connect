using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Models
{
    public class RSVP
    {
        [Key]
        public string RSVPId { get; set; }

        [Required]
        public string EventId { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
