using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Application.User.Mappers;
using TaskVisualizerWeb.Contracts.User.Response;
using TaskVisualizerWeb.Domain.Models.User;

namespace TaskVisualizerWeb.Application;

public class UserService(IUserRepository repository) : IUserService
{
    private readonly IUserRepository _repository = repository;

    public async Task<UserResponse> AddAsync(Contracts.User.Request.CreateUserRequest request)
    {
        var user =  await _repository.AddAsync(request.ToDomain());

        return user.ToContract();
    }

    public async Task<UserResponse> GetAsync(int id)
    {
        var user = await _repository.GetAsync(id);
        if (user is null)
            throw new InvalidDataException($"User with id '{id}' does not exist");

        return user.ToContract();
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
       var users = await _repository.GetAllAsync();

       return users.Select(user => user.ToContract()).ToList();
    }

    public async Task<bool> Exists(int id) => await _repository.Exists(id);
}
