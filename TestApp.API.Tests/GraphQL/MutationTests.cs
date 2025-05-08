using Microsoft.EntityFrameworkCore;
using Moq;
using TestApp.API.GraphQL;
using TestApp.API;

public class MutationTests
{
  [Fact]
  public async Task AddUserAsync_Should_Add_User_To_Database()
  {
    // Arrange
    var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: "AddUserTest")
    .Options;

    var mockFactory = new Mock<IDbContextFactory<AppDbContext>>();
    mockFactory.Setup(f => f.CreateDbContext()).Returns(new AppDbContext(options));

    var mutation = new Mutation();
    string userName = "Alice";

    // Act
    var result = await mutation.AddUserAsync(userName, mockFactory.Object);

    // Assert
    using var context = new AppDbContext(options);
    var userInDb = await context.Users.FirstOrDefaultAsync(u => u.Name == userName);
    Assert.NotNull(userInDb); //Test fails if line 20 of Mutations.cs is commented out
    Assert.Equal(userName, result.Name);
  }
}