using Microsoft.AspNetCore.Mvc;
using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Contracts.User.Request;
using TaskVisualizerWeb.Contracts.User.Response;

namespace TaskVisualizerWeb.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(ILogger<UsersController> logger, IUserService userService) : ControllerBase
{
    private readonly ILogger<UsersController> _logger = logger;
    private readonly IUserService _userService = userService;

    [HttpGet("{id}")]
    public async Task<UserResponse> GetAsync(int id) => await _userService.GetAsync(id);

    [HttpGet]
    public async Task<List<UserResponse>> GetAllAsync() => await _userService.GetAllAsync();

    [HttpPost]
    public async Task<UserResponse> AddAsync(CreateUserRequest user) => await _userService.AddAsync(user);
}
