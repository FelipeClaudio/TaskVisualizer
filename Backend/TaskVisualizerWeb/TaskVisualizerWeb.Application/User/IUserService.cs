namespace TaskVisualizerWeb.Application.User;
using User = Contracts.User;

public interface IUserService
{
    Task<List<User>> GetAllAsync();
    Task<User> GetAsync(int id);
    Task<User> AddAsync(User User);
}
