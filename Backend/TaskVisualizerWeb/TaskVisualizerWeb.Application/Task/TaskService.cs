using TaskVisualizerWeb.Contracts.Task.Request;
using TaskVisualizerWeb.Contracts.Task.Response;
using TaskVisualizerWeb.Domain.Models.Task;

namespace TaskVisualizerWeb.Application.Task;

public class TaskService(ITaskRepository taskRepository) : ITaskService
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<TaskResponse> AddAsync(TaskCreationRequest taskToBeAdded)
    {
        var domainTask = new Domain.Models.Task.Task
        {
            Name = taskToBeAdded.Name,
            Description = taskToBeAdded.Description,
            DueDate = taskToBeAdded.DueDate,
            Statuses = [new() { Id = 1, ValidTo = null, StatusEnum = (TaskStatusEnum)taskToBeAdded.TaskStatus }],
            Points = taskToBeAdded.Points,
            UserId = taskToBeAdded.UserId,
        };

        var addedTask = await _taskRepository.AddAsync(domainTask);

        var taskResponse = new TaskResponse(
            addedTask.Id,
            addedTask.Name,
            addedTask.Description,
            addedTask.DueDate,
            addedTask.Points,
            (Contracts.Task.Commons.TaskStatusEnum)(addedTask.Statuses.LastOrDefault()?.StatusEnum ?? TaskStatusEnum.NotStarted),
            addedTask.UserId);

        return taskResponse;
    }

    public async Task<TaskResponse> GetAsync(int id)
    {
        var task = await _taskRepository.GetAsync(id);
        if (task is null)
            throw new InvalidDataException($"Task with id '{id}' does not exist");

        var lastStatus = task.Statuses.LastOrDefault();

        return new TaskResponse(
            task.Id, 
            task.Name, 
            task.Description, 
            task.DueDate, 
            task.Points, 
            (Contracts.Task.Commons.TaskStatusEnum)(lastStatus?.StatusEnum ?? TaskStatusEnum.NotStarted), 
            task.UserId);
    }
}
