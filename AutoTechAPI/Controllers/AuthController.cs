using AutoTechAPI.Interfaces;
using AutoTechAPI.Models;
using AutoTechAPI.Services;
using Google.Apis.Auth;
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

            var token = _tokenService.GenerateToken(user);

            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var res = await _userRepository.GetUserByEmail(user.Email);

            if (res == null)
            {
                return BadRequest("Usuário Não Encontrado");
            }

            if (user.Email == res.Email && _passwordService.VerifyHashPassword(user.HashPassword, res.HashPassword))
            {
                var token = _tokenService.GenerateToken(res);

                return Ok(new { token });
            }

            return BadRequest();
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Credential);

            User user = await _userRepository.GetUserByEmail(payload.Email);
;
            if (user == null)
            {
                User newUser = new User { Email = payload.Email, HashPassword = "" };
               await _userRepository.CreateUser(newUser);
                var token = _tokenService.GenerateToken(newUser);

               return Ok(new { tokenId = token });
            }
            else
            {
              var token = _tokenService.GenerateToken(user);
               return Ok(new { tokenId = token });
           }
        }

    }
}
