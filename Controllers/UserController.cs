using Microsoft.AspNetCore.Authorization; // Needed for [Authorize] attribute
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; // Needed for ClaimTypes

namespace AuthApi.Controllers
{
    [Route("api/[controller]")] // Base route: /api/User
    [ApiController] // Indicates this is an API controller
    [Authorize] // Applies authentication requirement to all actions in this controller
    public class UserController : ControllerBase
    {
        // GET /api/User/profile
        // Requires any authenticated user
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            // Access user claims from the authenticated HttpContext.User
            // These claims were added to the JWT during login and extracted by the JWT middleware.
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value; // This will be "User" or "Admin"

            // Return user information as a JSON object
            return Ok(new
            {
                Message = "This is a protected endpoint! You are authenticated.",
                UserId = userId,
                Username = username,
                Email = email,
                Role = role
            });
        }

        // GET /api/User/admin-data
        // Requires an authenticated user with the "Admin" role
        [HttpGet("admin-data")]
        [Authorize(Roles = "Admin")] // This explicitly checks for the "Admin" role claim
        public IActionResult GetAdminData()
        {
            // You can also get admin's claims here if needed
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            return Ok($"Hello, Admin {username}! This data is only accessible to users with the 'Admin' role.");
        }

        // GET /api/User/user-data
        // Requires an authenticated user with the "User" role
        [HttpGet("user-data")]
        [Authorize(Roles = "User")] // This explicitly checks for the "User" role claim
        public IActionResult GetUserData()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            return Ok($"Hello, User {username}! This data is accessible to general users.");
        }
    }
}
