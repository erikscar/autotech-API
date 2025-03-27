using AutoTechAPI.Interfaces;
using AutoTechAPI.Models;
using AutoTechAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AutoTechAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordService _passwordService;

        public UserController(IUserRepository userRepository, PasswordService passwordService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        [HttpGet]
        [Authorize]
        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<User> GetUserByToken()
        {
            var id = int.Parse(User.FindFirst("UserId")?.Value);

            return await _userRepository.GetUserById(id);
        }

        [HttpPost("email")]
        public async Task<IActionResult> SearchUserByEmail([FromBody] string email)
        {

            User user = await _userRepository.GetUserByEmail(email);

            if(user == null)
            {
                return (BadRequest("Email Informado Invalido!"));   
            }

            return Ok(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await _userRepository.CreateUser(user);
            await _userRepository.SaveAll();
            return StatusCode(201, user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            User userToUpdate = await _userRepository.GetUserByEmail(user.Email);

            try
            {
                userToUpdate.HashPassword = _passwordService.HashPassword(user.HashPassword);

                _userRepository.UpdateUser(userToUpdate);
                await _userRepository.SaveAll();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
            await _userRepository.SaveAll();
            return StatusCode(200);
        }
    }
}
