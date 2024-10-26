using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Domain;
using User = TaskVisulaizerWeb.Contracts.User;

namespace TaskVisualizerWeb.Application;

public class UserService(IUserRepository repository) : IUserService
{
    private readonly IUserRepository _repository = repository;

    public TaskVisulaizerWeb.Contracts.User Add(TaskVisulaizerWeb.Contracts.User User)
    {
        var user =  _repository.Add(
            new Domain.Models.User.User { 
                Name = User.Name, 
                Email = User.Email, 
                Status = (Domain.Models.User.UserStatusEnum)User.Status 
            });

        return new TaskVisulaizerWeb.Contracts.User(user.Name, user.Email, (TaskVisulaizerWeb.Contracts.UserStatusEnum)user.Status);
    }

    public TaskVisulaizerWeb.Contracts.User Get(int id)
    {
        var user = _repository.Get(id);
        if (user is null)
            throw new InvalidDataException($"User with id '{id}' does not exist");

        return new TaskVisulaizerWeb.Contracts.User(user.Name, user.Email, (TaskVisulaizerWeb.Contracts.UserStatusEnum)user.Status);
    }
}
