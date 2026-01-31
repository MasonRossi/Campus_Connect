using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusConnect.Models
{
    public class RSVP
    {
        [Key]
        public string RSVPId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string EventId { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
