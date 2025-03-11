using AutoTechAPI.Models;

namespace AutoTechAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task CreateUser(User user);
        void UpdateUser(User user);
        Task DeleteUser(int id);
        Task SaveAll();
    }
}
