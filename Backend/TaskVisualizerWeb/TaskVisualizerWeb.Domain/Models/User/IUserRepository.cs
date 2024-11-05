namespace TaskVisualizerWeb.Domain.Models.User;

public interface IUserRepository
{
    List<User> GetAll();
    User Add(User user);
    User? Get(int id);
}