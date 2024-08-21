using System.ComponentModel.DataAnnotations;

namespace AspDotNetCoreProject.Model.Account
{
    public class LoginModel
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
