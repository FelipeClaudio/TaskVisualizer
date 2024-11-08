using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Domain.Models.User;
using UserStatusEnum = TaskVisualizerWeb.Domain.Models.User.UserStatusEnum;

namespace TaskVisualizerWeb.Application;

public class UserService(IUserRepository repository) : IUserService
{
    private readonly IUserRepository _repository = repository;

    public async Task<Contracts.User.User> AddAsync(Contracts.User.User User)
    {
        var user =  await _repository.AddAsync(
            new Domain.Models.User.User { 
                Name = User.Name, 
                Email = User.Email, 
                Status = (UserStatusEnum)User.Status 
            });

        return new Contracts.User.User(user.Name, user.Email, (Contracts.User.UserStatusEnum)user.Status);
    }

    public async Task<Contracts.User.User> GetAsync(int id)
    {
        var user = await _repository.GetAsync(id);
        if (user is null)
            throw new InvalidDataException($"User with id '{id}' does not exist");

        return new Contracts.User.User(user.Name, user.Email, (Contracts.User.UserStatusEnum)user.Status);
    }

    public async Task<List<Contracts.User.User>> GetAllAsync()
    {
       var users = await _repository.GetAllAsync();

        return users.Select(user => new Contracts.User.User { 
            Email = user.Email, 
            Name = user.Name, 
            Status = (Contracts.User.UserStatusEnum)user.Status }
        ).ToList();
    }
}
