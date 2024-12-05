using FluentAssertions;
using System.Net.Http.Json;
using TaskVisualizerWeb.Contracts.User.Commons;
using TaskVisualizerWeb.Contracts.User.Request;
using TaskVisualizerWeb.Contracts.User.Response;

namespace TaskVisualzierWeb.IntegrationTests;

public sealed class UsersTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
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
        var user = new CreateUserRequest("Test User 2", "test2@test.com", UserStatusEnum.Active);
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/users", user);
        var addedUser = await response.Content.ReadFromJsonAsync<UserResponse>();

        // Act
        var result = await client.GetAsync($"users/{addedUser.Id}");
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

    [Fact]
    public async Task GetAllAsync_MultipleUser_ShouldReturnAllUserOnDatabase()
    {
        // Arrange
        var client = _factory.CreateClient();
        const int numberOfUsers = 4;
        var taskList = new List<Task>();

        for (int i = 0; i < numberOfUsers; i++)
        {
            var user = new CreateUserRequest($"Test user{i}", $"test{i}@test.com", UserStatusEnum.Active);
            taskList.Add(client.PostAsJsonAsync("/users", user));
        }
        await Task.WhenAll(taskList);

        // Act
        var result = await client.GetAsync("users");
        var userResponse = await result.Content.ReadFromJsonAsync<List<UserResponse>>();

        // Assert
        userResponse.Should().HaveCount(numberOfUsers);
    }

    [Fact]
    public async Task GetAllAsync_NoUser_ShouldReturnEmptyResult()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var result = await client.GetAsync("users");
        var userResponse = await result.Content.ReadFromJsonAsync<List<UserResponse>>();

        // Assert
        userResponse.Should().BeEmpty();
    }
}
