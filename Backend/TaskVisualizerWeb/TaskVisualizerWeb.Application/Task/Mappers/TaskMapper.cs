using TaskVisualizerWeb.Contracts.Task.Request;
using TaskVisualizerWeb.Contracts.Task.Response;
using TaskVisualizerWeb.Domain.Models.Task;

namespace TaskVisualizerWeb.Application.Task.Mappers;
public static class TaskMapper
{
    public static Domain.Models.Task.Task ToDomain(this TaskCreationRequest task) =>
        new()
        {
            Name = task.Name,
            Description = task.Description,
            DueDate = task.DueDate,
            Statuses = [new() { ValidTo = null, StatusEnum = (TaskStatusEnum)task.TaskStatus }],
            Points = task.Points,
            UserId = task.UserId,
        };

    public static TaskResponse ToContract(this Domain.Models.Task.Task task) =>
        new
        (
            task.Id,
            task.Name,
            task.Description,
            task.DueDate,
            task.Points,
            (Contracts.Task.Commons.TaskStatusEnum)(task.Statuses.LastOrDefault()?.StatusEnum ?? TaskStatusEnum.NotStarted),
            task.UserId
        );
}
