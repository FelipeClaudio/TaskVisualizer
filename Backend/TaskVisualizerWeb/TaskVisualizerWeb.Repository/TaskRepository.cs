using Microsoft.EntityFrameworkCore;
using TaskVisualizerWeb.Domain.Models.Task;

namespace TaskVisualizerWeb.Repository;

public class TaskRepository(EfCorePostgreContext context) : ITaskRepository
{
    private readonly EfCorePostgreContext _dbContext = context;

    public async Task<Domain.Models.Task.Task> AddAsync(Domain.Models.Task.Task task)
    {
        var createdTask = await _dbContext.AddAsync(task);
        await _dbContext.SaveChangesAsync();

        return createdTask.Entity;
    }

    public async Task<Domain.Models.Task.Task?> GetAsync(int id) => await _dbContext.Tasks.SingleOrDefaultAsync(t => t.Id == id);
}
