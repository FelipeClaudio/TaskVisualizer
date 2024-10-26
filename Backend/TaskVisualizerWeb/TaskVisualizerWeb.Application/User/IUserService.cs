namespace TaskVisualizerWeb.Application.User;
using User = Contracts.User;

public interface IUserService
{
    User Get(int id);
    User Add(User User);
}
