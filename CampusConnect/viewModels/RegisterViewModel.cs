namespace CampusConnect.viewModels
{
    public class RegisterViewModel
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Student" or "Organizer"
        public string ErrorMessage { get; set; }
    }
}
