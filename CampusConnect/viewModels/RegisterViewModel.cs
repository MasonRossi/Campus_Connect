using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CampusConnect.viewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Display Name is required.")]
        [StringLength(50, ErrorMessage = "Display Name cannot exceed 50 characters.")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress (ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        [ValidateNever]
        public string? ErrorMessage { get; set; }
    }

}
