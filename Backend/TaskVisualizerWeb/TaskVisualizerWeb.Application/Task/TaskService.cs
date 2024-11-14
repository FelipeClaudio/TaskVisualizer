using TaskVisualizerWeb.Application.Task.Mappers;
using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Contracts.Task.Request;
using TaskVisualizerWeb.Contracts.Task.Response;
using TaskVisualizerWeb.Domain.Models.Task;
using TaskVisualizerWeb.Domain.Models.User;

namespace TaskVisualizerWeb.Application.Task;

public class TaskService(ITaskRepository taskRepository, IUserService userService) : ITaskService
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly IUserService _userService = userService;

    public async Task<TaskResponse> AddAsync(TaskCreationRequest taskToBeAdded)
    {
        var userExists = await _userService.Exists(taskToBeAdded.UserId);

        if (!userExists)
            throw new InvalidDataException($"User with id {taskToBeAdded.UserId} not found");

        var domainTask = taskToBeAdded.ToDomain();

        var addedTask = await _taskRepository.AddAsync(domainTask);

        return addedTask.ToContract();
    }

    public async Task<List<TaskResponse>> GetAllForUserAsync(int userId)
    {
        var userExists = await _userService.Exists(userId);

        if (!userExists)
            throw new InvalidDataException($"User with id {userId} not found");

        var userTasks = await _taskRepository.GetAllForUserAsync(userId);

        if (userTasks is null)
            return [];

        return userTasks
            .Select(task => task.ToContract())
            .ToList();
    }

    public async Task<TaskResponse> GetAsync(int id)
    {
        var task = await _taskRepository.GetAsync(id);
        if (task is null)
            throw new InvalidDataException($"Task with id '{id}' does not exist");

        return task.ToContract();
    }
}
