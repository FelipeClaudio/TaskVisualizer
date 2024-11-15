using FluentAssertions;
using FluentValidation;
using Moq;
using TaskVisualizerWeb.Application.Task;
using TaskVisualizerWeb.Application.Task.Mappers;
using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Contracts.Task.Request;
using TaskVisualizerWeb.Domain.Models.Task;

namespace TestVisualizerWeb.UnitTests.Application.Task;

public sealed class TaskServiceTests
{
    [Fact]
    public async System.Threading.Tasks.Task CreateTask_ValidInput_ShouldCreateTask()
    {
        // Arrange
        var repositoryMock = new Mock<ITaskRepository>();
        var taskToBeAdded = new TaskCreationRequest(
            "Create task visualizer app",
            "Creation of this nice app",
            new DateTime(2025, 03, 01),
            13,
            TaskVisualizerWeb.Contracts.Task.Commons.TaskStatusEnum.InProgress,
            1);

        var response = taskToBeAdded.ToDomain();
        repositoryMock.Setup(tr => tr.AddAsync(It.Is<TaskVisualizerWeb.Domain.Models.Task.Task>(t =>
            t.Name == taskToBeAdded.Name &&
            t.Description == taskToBeAdded.Description &&
            t.DueDate == taskToBeAdded.DueDate &&
            t.UserId == taskToBeAdded.UserId &&
            t.Points == taskToBeAdded.Points &&
            t.Statuses[0].StatusEnum == (TaskStatusEnum)taskToBeAdded.TaskStatus)))
            .ReturnsAsync(response);

        var userServiceMock = new Mock<IUserService>();
        userServiceMock.Setup(u => u.Exists(taskToBeAdded.UserId))
            .ReturnsAsync(true);

        var service = new TaskService(repositoryMock.Object, new TaskValidator(), userServiceMock.Object);

        // Act
        var createdTask = await service.AddAsync(taskToBeAdded);

        // Assert
        createdTask.Should().BeEquivalentTo(taskToBeAdded);
    }
}
