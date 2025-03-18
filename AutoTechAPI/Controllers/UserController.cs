using AutoTechAPI.Interfaces;
using AutoTechAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AutoTechAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await _userRepository.CreateUser(user);
            await _userRepository.SaveAll();
            return StatusCode(201, user);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            _userRepository.UpdateUser(user);
            await _userRepository.SaveAll();
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
