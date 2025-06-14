using System.ComponentModel.DataAnnotations;

namespace AuthApi.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Username or Email is required.")]
        public string UsernameOrEmail { get; set; } = string.Empty; // Allow login with username or email

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;
    }
}
