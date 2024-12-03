using FluentValidation;
using TaskVisualizerWeb.Application.Task.Mappers;
using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Contracts.Task.Request;
using TaskVisualizerWeb.Contracts.Task.Response;
using TaskVisualizerWeb.Domain.Exceptions;
using TaskVisualizerWeb.Domain.Models.Task;

namespace TaskVisualizerWeb.Application.Task;

public class TaskService(ITaskRepository taskRepository, IValidator<Domain.Models.Task.Task> validator, IUserService userService) : ITaskService
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly IValidator<Domain.Models.Task.Task> _validator = validator;
    private readonly IUserService _userService = userService;

    public async Task<TaskResponse> AddAsync(TaskCreationRequest taskToBeAdded)
    {
        var userExists = await _userService.Exists(taskToBeAdded.UserId);

        if (!userExists)
            throw new ResourceNotFoundException($"User with id {taskToBeAdded.UserId} not found");

        var domainTask = taskToBeAdded.ToDomain();
        await _validator.ValidateAndThrowAsync(domainTask);

        var addedTask = await _taskRepository.AddAsync(domainTask);

        return addedTask.ToContract();
    }

    public async Task<List<TaskResponse>> GetAllForUserAsync(int userId)
    {
        var userExists = await _userService.Exists(userId);

        if (!userExists)
            throw new ResourceNotFoundException($"User with id {userId} not found");

        var userTasks = await _taskRepository.GetAllForUserAsync(userId);

        if (userTasks is null || userTasks.Count == 0)
            return [];

        return userTasks
            .Select(task => task.ToContract())
            .ToList();
    }

    public async Task<TaskResponse> GetAsync(int id)
    {
        var task = await _taskRepository.GetAsync(id);
        if (task is null)
            throw new ResourceNotFoundException($"Task with id '{id}' does not exist");

        return task.ToContract();
    }

    public async Task<TaskResponse> UpdateTaskStatus(TaskStatusUpdateRequest taskStatusUpdateRequest)
    {
        var taskExists = await _taskRepository.ExistsAsync(taskStatusUpdateRequest.Id);
        if (!taskExists)
            throw new ResourceNotFoundException($"Task with id '{taskStatusUpdateRequest.Id}' does not exist");

        var updatedTask = await _taskRepository.UpdateAsync(taskStatusUpdateRequest.Id, (TaskStatusEnum)taskStatusUpdateRequest.TaskStatus);

        return updatedTask.ToContract();
    }
}
