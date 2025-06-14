using AuthApi.Data;
using AuthApi.DTOs;
using AuthApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt; // For JwtSecurityTokenHandler
using Microsoft.IdentityModel.Tokens; // For SymmetricSecurityKey, SigningCredentials, SecurityTokenDescriptor
using System.Text; // For Encoding.UTF8
using Microsoft.Extensions.Configuration; // For accessing JWT settings
using BCrypt.Net; // For password hashing functions

namespace AuthApi.Controllers
{
    [Route("api/[controller]")] // Sets the base route for this controller (e.g., /api/Auth)
    [ApiController] // Indicates that this controller responds to web API requests
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        // Constructor for Dependency Injection:
        // The DbContext and Configuration are automatically provided by the ASP.NET Core framework.
        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST /api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            // Validate incoming DTO (handled by [ApiController] and model validation attributes)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if username already exists in the database
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return BadRequest("Username already exists.");
            }

            // Check if email already exists in the database
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest("Email already exists.");
            }

            // Generate a unique salt for password hashing
            // BCrypt.Net.BCrypt.GenerateSalt() returns a string salt.
            string passwordSaltString = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the user's password using the generated salt
            // BCrypt.Net.BCrypt.HashPassword() returns the hashed password as a string,
            // which internally includes the salt.
            string passwordHashString = BCrypt.Net.BCrypt.HashPassword(request.Password, passwordSaltString);

            // Convert the string hash and salt to byte arrays for storage in the database
            // This matches the byte[] types in your User model.
            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = Encoding.UTF8.GetBytes(passwordHashString), // Store as bytes
                PasswordSalt = Encoding.UTF8.GetBytes(passwordSaltString), // Store as bytes
                Role = "User" // Assign a default role for new users
            };

            // Add the new user to the database context and save changes
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Return a success response
            return Ok("Registration successful!");
        }

        // POST /api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            // Validate incoming DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the user by username OR email in the database
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.UsernameOrEmail || u.Email == request.UsernameOrEmail);

            // If user not found, return unauthorized
            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            // Verify the provided password against the stored hashed password
            // BCrypt.Net.BCrypt.Verify automatically handles the salt which is embedded in the hash string.
            // We need to convert the stored byte[] PasswordHash back to a string for verification.
            if (!BCrypt.Net.BCrypt.Verify(request.Password, Encoding.UTF8.GetString(user.PasswordHash)))
            {
                return Unauthorized("Invalid credentials.");
            }

            // If password is correct, generate a JWT
            var token = CreateToken(user);

            // Return the JWT to the client
            return Ok(new { token = token });
        }

        // Helper method to create a JWT
        private string CreateToken(User user)
        {
            // Define claims (pieces of information) to be included in the token payload
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Unique user ID
                new Claim(ClaimTypes.Name, user.Username),               // Username
                new Claim(ClaimTypes.Email, user.Email),                 // Email
                new Claim(ClaimTypes.Role, user.Role)                    // User's role
            };

            // Retrieve the JWT secret key from configuration
            var secretKey = _configuration["JwtSettings:Secret"];

            // Critical: Ensure the secret key is configured. If not, throw an exception.
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT Secret key is not configured in appsettings.json.");
            }

            // Create a symmetric security key from the secret bytes
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // Create signing credentials using the key and HMAC SHA256 algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            // Define the token's properties (subject, expiry, signing credentials, issuer, audience)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // The claims for the token
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiryMinutes"])), // Token expiry time
                SigningCredentials = creds, // The signing credentials
                Issuer = _configuration["JwtSettings:Issuer"], // The issuer of the token
                Audience = _configuration["JwtSettings:Audience"] // The audience for whom the token is intended
            };

            // Create and write the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Serialize the token into a string format
            return tokenHandler.WriteToken(token);
        }
    }
}
