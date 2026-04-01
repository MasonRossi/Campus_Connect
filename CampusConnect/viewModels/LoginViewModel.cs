using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CampusConnect.viewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Display Name is required.")]
        [StringLength(50, ErrorMessage = "Display Name cannot exceed 50 characters.")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ValidateNever]
        public string? ErrorMessage { get; set; }
    }

}
