using TaskVisualizerWeb.Contracts.User.Commons;

namespace TaskVisualizerWeb.Contracts.User.Response;
public readonly record struct UserResponse(int Id, string Name, string Email, UserStatusEnum Status);
