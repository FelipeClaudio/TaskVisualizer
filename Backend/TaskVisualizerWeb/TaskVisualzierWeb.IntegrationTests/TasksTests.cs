using FizzWare.NBuilder;
using FluentAssertions;
using System.Net.Http.Json;
using TaskVisualizerWeb.Contracts.Task.Request;
using TaskVisualizerWeb.Contracts.Task.Response;
using TaskVisualizerWeb.Contracts.User.Commons;
using TaskVisualizerWeb.Contracts.User.Request;
using TaskVisualizerWeb.Domain.Models.User;

namespace TaskVisualzierWeb.IntegrationTests;

public sealed class TasksTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private readonly IntegrationTestWebAppFactory _factory = factory;
    private User _user = null;

    [Fact]
    public async Task GetAllForUserAsync_NoTaskForGivenUser_ShouldReturnEmptyResult()
    {
        // Arrange
        await SeedUsersAsync();
        var client = _factory.CreateClient();

        // Act
        var result = await client.GetAsync($"/tasks/users/{_user.Id}");

        // Assert
        result.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
    }

    private async Task SeedUsersAsync()
    {
        var user = Builder<User>.CreateNew().Build();
        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();
        _user = user;
    }
}
