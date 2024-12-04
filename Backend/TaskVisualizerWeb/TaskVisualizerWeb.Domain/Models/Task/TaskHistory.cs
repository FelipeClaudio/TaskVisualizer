using TaskVisualizerWeb.Domain.Models.Commons;

namespace TaskVisualizerWeb.Domain.Models.Task;
public sealed class TaskHistory : Entity
{
    public DateTime? ValidTo { get; set; }
    public TaskStatusEnum StatusEnum { get; set; }
}

public enum TaskStatusEnum
{
    NotStarted = 0,
    InProgress = 1,
    Done = 2,
    Blocked = 3,
    Canceled = 4,
}