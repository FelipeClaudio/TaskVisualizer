using Microsoft.AspNetCore.Mvc;
using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Contracts.User;

namespace TaskVisualizerWeb.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(ILogger<UsersController> logger, IUserService userService) : ControllerBase
{
    private readonly ILogger<UsersController> _logger = logger;
    private readonly IUserService _userService = userService;

    [HttpGet("{id}")]
    public async Task<User> GetAsync(int id) => await _userService.GetAsync(id);

    [HttpGet]
    public async Task<List<User>> GetAllAsync() => await _userService.GetAllAsync();

    [HttpPost]
    public async Task<User> AddAsync(User user) => await _userService.AddAsync(user);
}
