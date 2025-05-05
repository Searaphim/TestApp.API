namespace TestApp.API
{
    using Microsoft.EntityFrameworkCore;
    using TestApp.API.Models;
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
    }
}
