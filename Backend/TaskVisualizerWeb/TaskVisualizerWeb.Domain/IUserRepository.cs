using TaskVisualizerWeb.Domain.Models.User;

namespace TaskVisualizerWeb.Domain;

public interface IUserRepository
{
    List<User> GetAll();
    User Add(User user);
    User? Get(int id);
}