using AutoTechAPI.Data;
using AutoTechAPI.Interfaces;
using AutoTechAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutoTechAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AutoTechDbContext _context;

        public UserRepository(AutoTechDbContext context)
        {
            _context = context;
        }

        public async Task CreateUser(User user)
        {
           await _context.Users.AddAsync(user);
        }

        public async Task DeleteUser(int id)
        {
            User userToDelete = await _context.Users.FindAsync(id);
            _context.Users.Remove(userToDelete);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public void UpdateUser(User user)
        {
             _context.Entry(user).State = EntityState.Modified;
        }

        public async Task SaveAll()
        {
            await _context.SaveChangesAsync();
        }
    }
}
