using TaskVisualizerWeb.Contracts.Task.Commons;

namespace TaskVisualizerWeb.Contracts.Task.Response;

public readonly record struct TaskResponse(int Id, string Name, string Description, DateTime DueDate, ushort Points, TaskStatusEnum TaskStatus, int UserId);

