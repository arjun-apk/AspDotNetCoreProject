using System.ComponentModel.DataAnnotations;

namespace AspDotNetCoreProject.Model.Account
{
    public class RegisterModel
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
    }
}
