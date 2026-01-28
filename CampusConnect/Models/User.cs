using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // "Student" or "Organizer"
    }
}
