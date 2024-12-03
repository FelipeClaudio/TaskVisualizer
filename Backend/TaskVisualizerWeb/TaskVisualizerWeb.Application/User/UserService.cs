using FluentValidation;
using TaskVisualizerWeb.Application.User;
using TaskVisualizerWeb.Application.User.Mappers;
using TaskVisualizerWeb.Contracts.User.Response;
using TaskVisualizerWeb.Domain.Exceptions;
using TaskVisualizerWeb.Domain.Models.User;

namespace TaskVisualizerWeb.Application;

public class UserService(IUserRepository repository, IValidator<Domain.Models.User.User> validator) : IUserService
{
    private readonly IUserRepository _repository = repository;
    private readonly IValidator<Domain.Models.User.User> _validator = validator;

    public async Task<UserResponse> AddAsync(Contracts.User.Request.CreateUserRequest request)
    {
        var domainUser = request.ToDomain();
        await _validator.ValidateAndThrowAsync(domainUser);

        var user =  await _repository.AddAsync(domainUser);

        return user.ToContract();
    }

    public async Task<UserResponse> GetAsync(int id)
    {
        var user = await _repository.GetAsync(id);
        if (user is null)
            throw new ResourceNotFoundException($"User with id '{id}' does not exist");

        return user.ToContract();
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
       var users = await _repository.GetAllAsync();

       return users.Select(user => user.ToContract()).ToList();
    }

    public async Task<bool> Exists(int id) => await _repository.ExistsAsync(id);
}
