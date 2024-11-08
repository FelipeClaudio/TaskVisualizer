namespace TaskVisualizerWeb.Contracts.Task;

public readonly record struct Task(string Name, string Description, DateTime DueDate, ushort Points, TaskStatusEnum TaskStatus, int UserId);

public enum TaskStatusEnum
{
    NotStarted = 0,
    InProgress = 1,
    Done = 2,
    Blocked = 3,
    Canceled = 4,
}