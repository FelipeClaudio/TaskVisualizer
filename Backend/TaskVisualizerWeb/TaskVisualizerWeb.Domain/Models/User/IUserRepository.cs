namespace TaskVisualizerWeb.Domain.Models.User;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User> AddAsync(User user);
    Task<User?> GetAsync(int id);
    Task<bool> ExistsAsync(int id);
}