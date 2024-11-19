using FluentAssertions;
using Moq;
using TaskVisualizerWeb.Application.Task;
using TaskVisualizerWeb.Application.Task.Mappers;
using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Contracts.Task.Request;
using TaskVisualizerWeb.Domain;
using TaskVisualizerWeb.Domain.Models.Task;

namespace TestVisualizerWeb.UnitTests.Application.Task;

public sealed class TaskServiceTests
{
    private TaskCreationRequest _taskToBeAdded;
    private Mock<ITaskRepository> _repositoryMock;
    private Mock<IUserService> _userServiceMock;

    public TaskServiceTests()
    {
        _taskToBeAdded = new TaskCreationRequest(
            "Create task visualizer app",
            "Creation of this nice app",
            new DateTime(2025, 03, 01),
            13,
            TaskVisualizerWeb.Contracts.Task.Commons.TaskStatusEnum.InProgress,
            1);

        _repositoryMock = new Mock<ITaskRepository>();
        _repositoryMock.Setup(tr => tr.AddAsync(It.Is<TaskVisualizerWeb.Domain.Models.Task.Task>(t =>
            t.Name                      ==  _taskToBeAdded.Name &&
            t.Description               ==  _taskToBeAdded.Description &&
            t.DueDate                   ==  _taskToBeAdded.DueDate &&
            t.UserId                    ==  _taskToBeAdded.UserId &&
            t.Points                    ==  _taskToBeAdded.Points &&
            t.Statuses[0].StatusEnum    ==  (TaskStatusEnum)_taskToBeAdded.TaskStatus)))
        .ReturnsAsync(_taskToBeAdded.ToDomain());

        _userServiceMock = new Mock<IUserService>();
        _userServiceMock.Setup(u => u.Exists(_taskToBeAdded.UserId))
            .ReturnsAsync(true);
    }

    [Fact]
    public async System.Threading.Tasks.Task CreateTask_ValidInput_ShouldCreateTask()
    {
        // Arrange
        var service = new TaskService(_repositoryMock.Object, new TaskValidator(new DateProvider()), _userServiceMock.Object);

        // Act
        var createdTask = await service.AddAsync(_taskToBeAdded);

        // Assert
        createdTask.Should().BeEquivalentTo(_taskToBeAdded);
    }

    [Fact]
    public async System.Threading.Tasks.Task CreateTask_NonExistingUser_ShouldReturnError()
    {
        // Arrange
        var service = new TaskService(_repositoryMock.Object, new TaskValidator(new DateProvider()), _userServiceMock.Object);
        _taskToBeAdded = _taskToBeAdded with { UserId = 5 };

        // Act
        Func<Task<TaskVisualizerWeb.Contracts.Task.Response.TaskResponse>> act = async () => await service.AddAsync(_taskToBeAdded);

        // Assert
        await act.Should().ThrowAsync<InvalidDataException>();
    }

    [Fact]
    public async System.Threading.Tasks.Task UpdateTaskStatus_ValidInput_ShouldUpdateTaskStatus()
    {
        // Arrange
        var service = new TaskService(_repositoryMock.Object, new TaskValidator(new DateProvider()), _userServiceMock.Object);
        var createdTask = await service.AddAsync(_taskToBeAdded) with { Id = 1 };
        var expectedStatus = TaskVisualizerWeb.Contracts.Task.Commons.TaskStatusEnum.Done;
        var taskStatusUpdateRequest = new TaskStatusUpdateRequest(createdTask.Id, expectedStatus);

        _repositoryMock.Setup(r => r.ExistsAsync(createdTask.Id)).ReturnsAsync(true);
        var updateResult = _taskToBeAdded.ToDomain();
        updateResult.Statuses.Add(new TaskHistory { StatusEnum = (TaskStatusEnum)expectedStatus, Id = 1 });
        _repositoryMock.Setup(r => r.UpdateAsync(createdTask.Id, (TaskStatusEnum)expectedStatus))
            .ReturnsAsync(updateResult);

        // Act
        var updatedTask = await service.UpdateTaskStatus(taskStatusUpdateRequest);

        // Assert
        updatedTask.TaskStatus.Should().Be(expectedStatus);
    }
}
