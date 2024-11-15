using TaskVisualizerWeb.Contracts.Task.Commons;

namespace TaskVisualizerWeb.Contracts.Task.Request;

public readonly record struct TaskStatusUpdateRequest(int Id, TaskStatusEnum TaskStatus);