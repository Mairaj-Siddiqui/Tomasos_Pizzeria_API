using Microsoft.AspNetCore.Mvc;
using Tomasos_Pizzeria_API.Data.Interfaces;
using TomasosPizzeriaAPI.Data.Entities;
using TomasosPizzeriaAPI.DTOs;

namespace Tomasos_Pizzeria_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                var existing = await _authRepo.GetByUsernameAsync(dto.Username);
                if (existing != null)
                    return BadRequest("Username already exists");

                var user = new User
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Role = "User"
                };

                var registered = await _authRepo.RegisterAsync(user, dto.Password);
                var token = await _authRepo.GenerateJwtTokenAsync(registered);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Registration failed: " + ex.Message);
            }
        }

        // POST: api/Auth/login

        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var user = await _authRepo.ValidateCredentialsAsync(dto.Username, dto.Password);
                if (user == null)
                    return Unauthorized("Invalid username or password");

                var token = await _authRepo.GenerateJwtTokenAsync(user);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Login failed: " + ex.Message);
            }
        }

    }
}
