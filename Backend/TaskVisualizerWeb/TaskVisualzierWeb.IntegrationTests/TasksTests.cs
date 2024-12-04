using FizzWare.NBuilder;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using TaskVisualizerWeb.Application.Task.Mappers;
using TaskVisualizerWeb.Contracts.Task.Request;
using TaskVisualizerWeb.Contracts.Task.Response;
using TaskVisualizerWeb.Domain.Models.Task;
using TaskVisualizerWeb.Domain.Models.User;
using Task = System.Threading.Tasks.Task;

namespace TaskVisualzierWeb.IntegrationTests;

public sealed class TasksTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private readonly IntegrationTestWebAppFactory _factory = factory;
    private User _user = null;
    private IList<TaskVisualizerWeb.Domain.Models.Task.Task> _tasks;

    [Fact]
    public async Task GetAsync_ValidTask_ShouldReturnTask()
    {
        // Arrange
        await SeedUsersAsync();
        await SeedTasksForUserAsync(_user.Id);
        var client = _factory.CreateClient();

        // Act
        var result = await client.GetAsync($"/tasks/{_tasks[1].Id}");
        var task = await result.Content.ReadFromJsonAsync<TaskResponse>();

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        task.Should().BeEquivalentTo(_tasks[1].ToContract());
    }

    [Fact]
    public async Task GetAsync_NonExistingTask_ShouldReturnNotFound()
    {
        // Arrange
        await SeedUsersAsync();
        var client = _factory.CreateClient();

        // Act
        var result = await client.GetAsync($"/tasks/1");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetAllForUserAsync_NoTaskForGivenUser_ShouldReturnEmptyResult()
    {
        // Arrange
        await SeedUsersAsync();
        var client = _factory.CreateClient();

        // Act
        var result = await client.GetAsync($"/tasks/users/{_user.Id}");

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task GetAllForUserAsync_WithMultipleTasksForUser_ShouldReturnTasks()
    {
        // Arrange
        await SeedUsersAsync();
        await SeedTasksForUserAsync(_user.Id);
        var client = _factory.CreateClient();

        // Act
        var result = await client.GetAsync($"/tasks/users/{_user.Id}");
        var userTasks = await result.Content.ReadFromJsonAsync<List<TaskResponse>>();

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        userTasks.Should().HaveCount(_tasks.Count);
    }

    [Fact]
    public async Task AddTask_ValidTaskAndValidUser_ShouldReturnAddedTaskInTheList()
    {
        // Arrange
        await SeedUsersAsync();
        var client = _factory.CreateClient();
        var taskToBeCreated = new TaskCreationRequest(
            "test task",
            "my nice test task",
            DateTime.Now.AddDays(3),
            5,
            TaskVisualizerWeb.Contracts.Task.Commons.TaskStatusEnum.InProgress,
            _user.Id);

        // Act
        var result = await client.PostAsJsonAsync($"/tasks", taskToBeCreated);
        var userTasks = await result.Content.ReadFromJsonAsync<TaskResponse>();

        // Assert
        userTasks.Should().BeEquivalentTo(taskToBeCreated);
    }

    [Fact]
    public async Task AddTask_InvalidUser_ShouldBadRequest()
    {
        // Arrange
        await SeedUsersAsync();
        var client = _factory.CreateClient();
        var taskToBeCreated = new TaskCreationRequest(
            "test task",
            "my nice test task",
            DateTime.Now.AddDays(3),
            5,
            TaskVisualizerWeb.Contracts.Task.Commons.TaskStatusEnum.InProgress,
            _user.Id + 1);

        // Act
        var result = await client.PostAsJsonAsync($"/tasks", taskToBeCreated);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private async Task SeedUsersAsync()
    {
        var user = Builder<User>.CreateNew().Build();
        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();
        _user = user;
    }

    private async Task SeedTasksForUserAsync(int userId)
    {
        var tasks = Builder<TaskVisualizerWeb.Domain.Models.Task.Task>.
            CreateListOfSize(3)
            .All()
            .With(t => t.UserId = userId)
            .With(t => t.Statuses = [.. Builder<TaskHistory>
                .CreateListOfSize(3)
                .All()
                .With(x => x.Id = 0)
                .Build()])
            .Build();

        await DbContext.Tasks.AddRangeAsync(tasks);
        await DbContext.SaveChangesAsync();
        _tasks = tasks;
    }
}
