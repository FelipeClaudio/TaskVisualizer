namespace TaskVisualizerWeb.Domain.Models.Task;
public interface ITaskRepository
{
    Task<Task> AddAsync(Task task);
}
