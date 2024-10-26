using TaskVisualizerWeb.Domain.Models.User;

namespace TaskVisualizerWeb.Domain;

public interface IUserRepository
{
    User Add(User user);
    User? Get(int id);
}