using TaskVisualizerWeb.Contracts.User.Commons;

namespace TaskVisualizerWeb.Contracts.User.Request;

public readonly record struct CreateUserRequest(string Name, string Email, UserStatusEnum Status);