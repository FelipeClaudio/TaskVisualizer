﻿namespace TaskVisualizerWeb.Domain.Models.Task;
public interface ITaskRepository
{
    Task<Task> AddAsync(Task task);
    Task<Task?> GetAsync(int id);
    Task<List<Task>?> GetAllForUserAsync(int userId);
    Task<bool> ExistsAsync(int id);
    Task<Task> UpdateAsync(int taskId, TaskStatusEnum taskStatus);
}
