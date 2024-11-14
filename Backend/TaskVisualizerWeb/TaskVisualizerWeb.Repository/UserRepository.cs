using Microsoft.EntityFrameworkCore;
using TaskVisualizerWeb.Domain.Models.User;

namespace TaskVisualizerWeb.Repository;

public class UserRepository(EfCorePostgreContext context) : IUserRepository
{
    private readonly EfCorePostgreContext _dbContext = context;

    public async Task<User> AddAsync(User user)
    {
        var createdUser = await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return createdUser.Entity;
    }

    public async Task<User?> GetAsync(int id) => await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

    public async Task<List<User>> GetAllAsync() => await _dbContext.Users.ToListAsync();

    public async Task<bool> Exists(int id) => await _dbContext.Users.AnyAsync(u => u.Id == id);
}
