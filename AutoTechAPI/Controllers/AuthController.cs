using AutoTechAPI.Interfaces;
using AutoTechAPI.Models;
using AutoTechAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoTechAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;
        private readonly PasswordService _passwordService;
        public AuthController(IUserRepository userRepository, TokenService tokenservice, PasswordService passwordService)
        {
            _userRepository = userRepository;
            _tokenService = tokenservice;
            _passwordService = passwordService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            user.HashPassword = _passwordService.HashPassword(user.HashPassword);
            await _userRepository.CreateUser(user);
            await _userRepository.SaveAll();

            return Ok(new { message = "User registered successfully: " + user.Name });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var res = await _userRepository.GetUserById(user.Id);

            if(res == null) 
            {
                return BadRequest();
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
