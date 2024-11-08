using TaskVisualizerWeb.Contracts.Task.Request;
using TaskVisualizerWeb.Contracts.Task.Response;

namespace TaskVisualizerWeb.Application.Task;
public interface ITaskService
{
    Task<TaskResponse> AddAsync(TaskCreationRequest taskTobeCreated);
}
