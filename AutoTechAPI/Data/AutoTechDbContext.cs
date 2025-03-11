using AutoTechAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoTechAPI.Data
{
    public class AutoTechDbContext : DbContext
    {
       public AutoTechDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}
