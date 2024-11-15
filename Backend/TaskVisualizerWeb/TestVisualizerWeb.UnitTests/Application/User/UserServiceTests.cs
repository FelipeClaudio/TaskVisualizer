using FluentAssertions;
using Moq;
using TaskVisualizerWeb.Application;
using TaskVisualizerWeb.Application.User.Mappers;
using TaskVisualizerWeb.Contracts.User.Request;
using TaskVisualizerWeb.Domain.Models.User;
using UserStatusEnum = TaskVisualizerWeb.Domain.Models.User.UserStatusEnum;

namespace TestVisualizerWeb.UnitTests.Application.User;

public sealed class UserServiceTests
{
    [Fact]
    public async System.Threading.Tasks.Task Add_ValidData_ReturnCreatedUser()
    {
        // Arrange
        var userToBeAdded = new CreateUserRequest("Test User", "test@test.com", TaskVisualizerWeb.Contracts.User.Commons.UserStatusEnum.Active);
        var response = userToBeAdded.ToDomain();

        var repositoryMock = new Mock<IUserRepository>();
        repositoryMock
            .Setup(ur => ur.AddAsync(It.Is<TaskVisualizerWeb.Domain.Models.User.User>(u => 
                u.Name == userToBeAdded.Name && 
                u.Email == userToBeAdded.Email &&
                u.Status == (UserStatusEnum) userToBeAdded.Status
                )))
            .ReturnsAsync(response);

        var service = new UserService(repositoryMock.Object, new UserValidator());

        // Act
        var createdUser = await service.AddAsync(userToBeAdded);

        // Assert
        createdUser.Should().BeEquivalentTo(userToBeAdded);
    }

    [Fact]
    public async System.Threading.Tasks.Task Exists_ExistingUser_ReturnTrue()
    {
        // Arrange
        var repositoryMock = new Mock<IUserRepository>();
        repositoryMock
            .Setup(ur => ur.Exists(123))
            .ReturnsAsync(true);

        var service = new UserService(repositoryMock.Object, new UserValidator());

        // Act
        var userSearchResult = await service.Exists(123);

        // Assert
        userSearchResult.Should().BeTrue();
    }
}