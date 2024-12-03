using Microsoft.AspNetCore.Mvc;
using TaskVisualizerWeb.Application.Task;
using TaskVisualizerWeb.Contracts.Task.Request;
using TaskVisualizerWeb.Contracts.Task.Response;

namespace TaskVisualizerWeb.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController(ILogger<UsersController> logger, ITaskService taskService) : ControllerBase
{
    private readonly ILogger<UsersController> _logger = logger;
    private readonly ITaskService _taskService = taskService;

    [HttpGet("{id}")]
    public async Task<TaskResponse> GetAsync(int id) => await _taskService.GetAsync(id);

    [HttpGet("users/{userId}")]
    [ProducesResponseType<List<TaskResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllForUserAsync(int userId)
    {
        var result = await _taskService.GetAllForUserAsync(userId);

        if (result is null || result.Count == 0)
            return NoContent();

        return Ok(result);
    }

    [HttpPost]
    public async Task<TaskResponse> AddAsync(TaskCreationRequest task) => await _taskService.AddAsync(task);

    [HttpPatch]
    public async Task<TaskResponse> UpdateTaskStatus(TaskStatusUpdateRequest taskToBeUpdated) => await _taskService.UpdateTaskStatus(taskToBeUpdated);
}
