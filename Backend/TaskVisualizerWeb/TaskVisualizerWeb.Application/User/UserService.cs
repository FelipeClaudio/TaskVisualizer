using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Domain.Models.User;

namespace TaskVisualizerWeb.Application;

public class UserService(IUserRepository repository) : IUserService
{
    private readonly IUserRepository _repository = repository;

    public async Task<Contracts.User> AddAsync(Contracts.User User)
    {
        var user =  await _repository.AddAsync(
            new Domain.Models.User.User { 
                Name = User.Name, 
                Email = User.Email, 
                Status = (Domain.Models.User.UserStatusEnum)User.Status 
            });

        return new Contracts.User(user.Name, user.Email, (Contracts.UserStatusEnum)user.Status);
    }

    public async Task<Contracts.User> GetAsync(int id)
    {
        var user = await _repository.GetAsync(id);
        if (user is null)
            throw new InvalidDataException($"User with id '{id}' does not exist");

        return new Contracts.User(user.Name, user.Email, (Contracts.UserStatusEnum)user.Status);
    }

    public async Task<List<Contracts.User>> GetAllAsync()
    {
       var users = await _repository.GetAllAsync();

        return users.Select(user => new Contracts.User { 
            Email = user.Email, 
            Name = user.Name, 
            Status = (Contracts.UserStatusEnum)user.Status }
        ).ToList();
    }
}
