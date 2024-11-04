using TaskVisualizerWeb.Domain;
using TaskVisualizerWeb.Domain.Models.User;

namespace TaskVisualizerWeb.Repository;

public class UserRepository(EfCorePostgreContext context) : IUserRepository
{
    private readonly EfCorePostgreContext _dbContext = context;

    public User Add(User user)
    {
        var createdUser = _dbContext.Add(user);
        _dbContext.SaveChanges();

        return createdUser.Entity;
    }

    public User? Get(int id) => _dbContext.Users.SingleOrDefault(u => u.Id == id);

    public List<User> GetAll() => _dbContext.Users.ToList();
}
