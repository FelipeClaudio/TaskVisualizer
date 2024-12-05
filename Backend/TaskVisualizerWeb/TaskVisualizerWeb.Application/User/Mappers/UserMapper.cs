using TaskVisualizerWeb.Contracts.User.Request;
using TaskVisualizerWeb.Contracts.User.Response;
using TaskVisualizerWeb.Domain.Models.User;

namespace TaskVisualizerWeb.Application.User.Mappers;
public static class UserMapper
{
    public static Domain.Models.User.User ToDomain(this CreateUserRequest request) =>
        new()
        {
            Name = request.Name,
            Email = request.Email,
            Status = (UserStatusEnum)request.Status
        };

    public static UserResponse ToContract(this Domain.Models.User.User user) =>
        new
        (
            user.Id,
            user.Name,
            user.Email,
            (Contracts.User.Commons.UserStatusEnum)user.Status
        );
}
