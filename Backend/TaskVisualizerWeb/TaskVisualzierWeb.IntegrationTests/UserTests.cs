using FluentAssertions;
using System.Net.Http.Json;
using TaskVisualizerWeb.Contracts.User.Commons;
using TaskVisualizerWeb.Contracts.User.Request;

namespace TaskVisualzierWeb.IntegrationTests;
public sealed class UserTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private readonly IntegrationTestWebAppFactory _factory = factory;

    [Fact]
    public async Task CreateUser_ValidData_ShouldReturnCreatedUser()
    {
        // Arrange
        var user = new CreateUserRequest("Test User", "test@test.com", UserStatusEnum.Active);
        var client = _factory.CreateClient();

        // Act
        var result = await client.PostAsJsonAsync("/users", user);
        var createdUser = await result.Content.ReadFromJsonAsync<CreateUserRequest>();

        // Assert
        createdUser.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task GetUser_ValidData_ShouldReturnCreatedUser()
    {
        // Arrange
        var user = new CreateUserRequest("Test User", "test@test.com", UserStatusEnum.Active);
        var client = _factory.CreateClient();
        await client.PostAsJsonAsync("/users", user);

        // Act
        var result = await client.GetAsync("users/1");
        var createdUser = await result.Content.ReadFromJsonAsync<CreateUserRequest>();

        // Assert
        createdUser.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task GetUser_InexistentUser_ShouldReturnNotFoundError()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var result = await client.GetAsync("users/1");

        // Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
