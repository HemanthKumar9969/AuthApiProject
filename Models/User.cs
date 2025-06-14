using System.ComponentModel.DataAnnotations;

namespace AuthApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public byte[] PasswordHash { get; set; } = new byte[0]; // Stores the hashed password as bytes

        [Required]
        public byte[] PasswordSalt { get; set; } = new byte[0];  // Stores the unique salt as bytes

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "User"; // Default role is "User"
    }
}
