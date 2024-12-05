using TaskVisualizerWeb.Domain.Models.Commons;

namespace TaskVisualizerWeb.Domain.Models.Task;
public sealed class TaskHistory : Entity
{
    public DateTime? ValidTo { get; set; }
    public TaskStatusEnum StatusEnum { get; set; }
}