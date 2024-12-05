using TaskVisualizerWeb.Domain.Models.Commons;

namespace TaskVisualizerWeb.Domain.Models.Task;

public sealed class Task : Entity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime DueDate { get; set; }
    public required ushort Points { get; set; }
    public List<TaskHistory> Statuses { get; set; } = [];
    public required int UserId { get; set; }
}