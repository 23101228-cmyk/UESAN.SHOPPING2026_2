using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UESAN.SHOPPING.CORE.Core.DTOs;
using UESAN.SHOPPING.CORE.Core.Interfaces;

namespace UESAN.SHOPPING.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Implementa los métodos para SignIn y Signup aquí, utilizando _userService
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginDTO login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Email and password are required.");
            }
            var user = await _userService.SignIn(login.Email, login.Password);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(UserCreateDTO userCreateDTO)
        {
            if (userCreateDTO == null)
            {
                return BadRequest("User data is required.");
            }
            var result = await _userService.Signup(userCreateDTO);
            if (result == 0) return BadRequest("Failed to create user.");
            return Ok(result);
        }
    }
}
