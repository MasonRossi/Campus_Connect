using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } 
        public ICollection<Event> CreatedEvents { get; set; }
        public ICollection<RSVP> RSVPs { get; set; }
    }
}
