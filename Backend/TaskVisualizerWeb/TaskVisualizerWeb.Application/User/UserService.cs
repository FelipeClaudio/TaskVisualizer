using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Contracts.User.Response;
using TaskVisualizerWeb.Domain.Models.User;
using UserStatusEnum = TaskVisualizerWeb.Domain.Models.User.UserStatusEnum;

namespace TaskVisualizerWeb.Application;

public class UserService(IUserRepository repository) : IUserService
{
    private readonly IUserRepository _repository = repository;

    public async Task<UserResponse> AddAsync(Contracts.User.Request.CreateUserRequest User)
    {
        var user =  await _repository.AddAsync(
            new Domain.Models.User.User { 
                Name = User.Name, 
                Email = User.Email, 
                Status = (UserStatusEnum)User.Status 
            });

        return new UserResponse(user.Id, user.Name, user.Email, (Contracts.User.Commons.UserStatusEnum)user.Status);
    }

    public async Task<UserResponse> GetAsync(int id)
    {
        var user = await _repository.GetAsync(id);
        if (user is null)
            throw new InvalidDataException($"User with id '{id}' does not exist");

        return new UserResponse(user.Id, user.Name, user.Email, (Contracts.User.Commons.UserStatusEnum)user.Status);
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
       var users = await _repository.GetAllAsync();

        return users.Select(user => new UserResponse
        {
            Id = user.Id,
            Email = user.Email, 
            Name = user.Name, 
            Status = (Contracts.User.Commons.UserStatusEnum)user.Status }
        ).ToList();
    }
}
