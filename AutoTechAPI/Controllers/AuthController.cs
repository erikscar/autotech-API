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
        public AuthController(IUserRepository userRepository, TokenService tokenservice)
        {
            _userRepository = userRepository;
            _tokenService = tokenservice;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            await _userRepository.CreateUser(user);

            return Ok();
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
