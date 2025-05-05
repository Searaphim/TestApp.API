
namespace TestApp.API
{
    using Microsoft.EntityFrameworkCore;
    using TestApp.API.GraphQL;
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddPooledDbContextFactory<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddGraphQLServer().AddQueryType<Query>().AddMutationType<Mutation>().AddProjections().AddFiltering().AddSorting();
            builder.Services.AddCors(options =>
            {
              options.AddPolicy("AllowFrontend",
                policy =>
                {
                  policy.WithOrigins("http://localhost:51059").AllowAnyHeader().AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");

            app.UseAuthorization();


            app.MapControllers();
            app.MapGraphQL();

            app.Run();
        }
    }
}
