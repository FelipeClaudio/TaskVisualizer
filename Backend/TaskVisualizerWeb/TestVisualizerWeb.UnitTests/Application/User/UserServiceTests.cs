using FluentAssertions;
using Moq;
using TaskVisualizerWeb.Application;
using TaskVisualizerWeb.Domain.Models.User;

namespace TestVisualizerWeb.UnitTests.Application.User;

public sealed class UserServiceTests
{
    [Fact]
    public async System.Threading.Tasks.Task Add_ValidData_ReturnCreatedUser()
    {
        // Arrange
        var repositoryMock = new Mock<IUserRepository>();
        var service = new UserService(repositoryMock.Object);
        var userToBeAdded = new TaskVisualizerWeb.Contracts.User.User("Test User", "test@test.com", TaskVisualizerWeb.Contracts.User.UserStatusEnum.Active);
        var response = new TaskVisualizerWeb.Domain.Models.User.User
        {
            Name = userToBeAdded.Name,
            Email = userToBeAdded.Email,
            Status = (UserStatusEnum)userToBeAdded.Status,
        };
        repositoryMock
            .Setup(ur => ur.AddAsync(It.Is<TaskVisualizerWeb.Domain.Models.User.User>(u => 
                u.Name == userToBeAdded.Name && 
                u.Email == userToBeAdded.Email &&
                u.Status == (UserStatusEnum) userToBeAdded.Status
                )))
            .ReturnsAsync(response);

        // Act
        var createdUser = await service.AddAsync(userToBeAdded);

        // Assert
        createdUser.Should().BeEquivalentTo(userToBeAdded);
    }
}