using FluentAssertions;
using System.Net.Http.Json;

namespace TaskVisualzierWeb.IntegrationTests;
public sealed class UserTests : BaseIntegrationTest
{
    private readonly IntegrationTestWebAppFactory _factory;

    public UserTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CreateUser_ValidData_ShouldReturnCreatedUser()
    {
        // Arrange
        var user = new TaskVisualizerWeb.Contracts.User("Test User", "test@test.com", TaskVisualizerWeb.Contracts.UserStatusEnum.Active);
        var client = _factory.CreateClient();
        
        // Act
        var result = await client.PostAsJsonAsync("/users", user);
        var createdUser = await result.Content.ReadFromJsonAsync<TaskVisualizerWeb.Contracts.User>();

        // Assert
        createdUser.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async Task GetUser_ValidData_ShouldReturnCreatedUser()
    {
        // Arrange
        var user = new TaskVisualizerWeb.Contracts.User("Test User", "test@test.com", TaskVisualizerWeb.Contracts.UserStatusEnum.Active);
        var client = _factory.CreateClient();
        await client.PostAsJsonAsync("/users", user);

        // Act
        var result = await client.GetAsync("users/1");
        var createdUser = await result.Content.ReadFromJsonAsync<TaskVisualizerWeb.Contracts.User>();

        // Assert
        createdUser.Should().BeEquivalentTo(user);
    }
}
