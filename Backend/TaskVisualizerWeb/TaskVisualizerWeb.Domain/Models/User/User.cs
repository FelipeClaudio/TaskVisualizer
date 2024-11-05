using TaskVisualizerWeb.Domain.Models.Commons;

namespace TaskVisualizerWeb.Domain.Models.User;

public sealed class User : Entity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public ushort TotalPoints { get; set; } = 0;
    public ushort CurrentPoints { get; set; } = 0;
    public required UserStatusEnum Status { get; set; }
}

public enum UserStatusEnum
{
    Active,
    Inactive,
}