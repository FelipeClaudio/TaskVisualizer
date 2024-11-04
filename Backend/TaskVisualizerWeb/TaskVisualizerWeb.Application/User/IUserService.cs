namespace TaskVisualizerWeb.Application.User;
using User = Contracts.User;

public interface IUserService
{
    List<User> GetAll();
    User Get(int id);
    User Add(User User);
}
