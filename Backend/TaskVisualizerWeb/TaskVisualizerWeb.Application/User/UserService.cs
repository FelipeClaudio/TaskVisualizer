using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Domain.Models.User;

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

        return new Contracts.User(user.Name, user.Email, (Contracts.UserStatusEnum)user.Status);
    }

    public Contracts.User Get(int id)
    {
        var user = _repository.Get(id);
        if (user is null)
            throw new InvalidDataException($"User with id '{id}' does not exist");

        return new Contracts.User(user.Name, user.Email, (Contracts.UserStatusEnum)user.Status);
    }

    public List<Contracts.User> GetAll()
    {
       var users = _repository.GetAll();

        return users.Select(user => new Contracts.User { 
            Email = user.Email, 
            Name = user.Name, 
            Status = (Contracts.UserStatusEnum)user.Status }
        ).ToList();
    }
}
