namespace TaskVisualizerWeb.Application.User;

using TaskVisualizerWeb.Contracts.User.Response;
using CreateUserRequest = Contracts.User.Request.CreateUserRequest;

public interface IUserService
{
    Task<List<UserResponse>> GetAllAsync();
    Task<UserResponse> GetAsync(int id);
    Task<UserResponse> AddAsync(CreateUserRequest User);
}
