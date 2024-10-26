using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Contracts;
using TaskVisualizerWeb.Domain;

namespace TaskVisualizerWeb.Application;

public class UserService(IUserRepository repository) : IUserService
{
    private readonly IUserRepository _repository = repository;

    public Contracts.User Add(Contracts.User User)
    {
        var user =  _repository.Add(
            new Domain.Models.User.User { 
                Name = User.Name, 
                Email = User.Email, 
                Status = (Domain.Models.User.UserStatusEnum)User.Status 
            });

        return new Contracts.User(user.Name, user.Email, (UserStatusEnum)user.Status);
    }

    public Contracts.User Get(int id)
    {
        var user = _repository.Get(id);
        if (user is null)
            throw new InvalidDataException($"User with id '{id}' does not exist");

        return new Contracts.User(user.Name, user.Email, (UserStatusEnum)user.Status);
    }
}
