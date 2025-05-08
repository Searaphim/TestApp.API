using Microsoft.EntityFrameworkCore;
using TestApp.API.Models;

namespace TestApp.API.GraphQL
{
    public class Mutation
    {
        public async Task<User> AddUserAsync(
            string name,
            [Service] IDbContextFactory<AppDbContext> dbContextFactory)
        {
            await using var dbContext = dbContextFactory.CreateDbContext();

            var user = new User
            {
                Name = name
            };

            dbContext.Users.Add(user);
            //await dbContext.SaveChangesAsync();

            return user;
        }
    }
}
