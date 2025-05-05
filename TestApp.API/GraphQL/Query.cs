namespace TestApp.API.GraphQL
{
    using Microsoft.EntityFrameworkCore;
    using TestApp.API.Models;
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<User> GetUsers([Service] IDbContextFactory<AppDbContext> dbContextFactory)
        {
            var context = dbContextFactory.CreateDbContext();
            return context.Users;
        }
    }
}
