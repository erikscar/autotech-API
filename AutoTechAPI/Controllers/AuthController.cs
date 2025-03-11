using AutoTechAPI.Interfaces;
using AutoTechAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace AutoTechAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await userRepository.GetUsers();
        }

        [HttpGet("{id}")]
        public async Task<User> GetUserById(int id)
        {
            return await userRepository.GetUserById(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await userRepository.CreateUser(user);
            await userRepository.SaveAll();
            return StatusCode(201, user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            userRepository.UpdateUser(user);
            await userRepository.SaveAll();
            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await userRepository.DeleteUser(id);
            await userRepository.SaveAll();
            return StatusCode(200);
        }
    }
}
