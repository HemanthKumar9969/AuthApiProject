using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                Message = "This is a protected endpoint! You are authenticated.",
                UserId = userId,
                Username = username,
                Email = email,
                Role = role
            });
        }

        [HttpGet("admin-data")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdminData()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            return Ok($"Hello, Admin {username}! This data is only accessible to users with the 'Admin' role.");
        }

        [HttpGet("user-data")]
        [Authorize(Roles = "User")]
        public IActionResult GetUserData()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            return Ok($"Hello, User {username}! This data is accessible to general users.");
        }
    }
}
