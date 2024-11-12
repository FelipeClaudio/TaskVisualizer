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

    [HttpPost]
    public async Task<TaskResponse> AddAsync(TaskCreationRequest task) => await _taskService.AddAsync(task);
}
