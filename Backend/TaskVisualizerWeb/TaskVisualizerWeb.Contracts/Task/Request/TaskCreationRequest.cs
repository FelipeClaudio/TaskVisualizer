using TaskVisualizerWeb.Contracts.Task.Commons;

namespace TaskVisualizerWeb.Contracts.Task.Request;

public readonly record struct TaskCreationRequest(string Name, string Description, DateTime DueDate, ushort Points, TaskStatusEnum TaskStatus, int UserId);