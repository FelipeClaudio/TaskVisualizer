using Microsoft.AspNetCore.Mvc;
using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Contracts;

namespace TaskVisualizerWeb.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(ILogger<UsersController> logger, IUserService userService) : ControllerBase
{
    private readonly ILogger<UsersController> _logger = logger;
    private readonly IUserService _userService = userService;

    [HttpGet(Name = "{id}")]
    public User Get(int id) => _userService.Get(id);

    [HttpPost]
    public User Add(User user) => _userService.Add(user);
}
