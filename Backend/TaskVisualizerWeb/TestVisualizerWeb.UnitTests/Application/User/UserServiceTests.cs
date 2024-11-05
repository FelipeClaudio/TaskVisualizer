using FluentAssertions;
using Moq;
using TaskVisualizerWeb.Application;
using TaskVisualizerWeb.Domain.Models.User;

namespace TestVisualizerWeb.UnitTests.Application.User;

public sealed class UserServiceTests
{
    [Fact]
    public void Add_ValidData_ReturnCreatedUser()
    {
        // Arrange
        var repositoryMock = new Mock<IUserRepository>();
        var service = new UserService(repositoryMock.Object);
        var userToBeAdded = new TaskVisualizerWeb.Contracts.User("Test User", "test@test.com", TaskVisualizerWeb.Contracts.UserStatusEnum.Active);
        var response = new TaskVisualizerWeb.Domain.Models.User.User
        {
            Name = userToBeAdded.Name,
            Email = userToBeAdded.Email,
            Status = (TaskVisualizerWeb.Domain.Models.User.UserStatusEnum)userToBeAdded.Status,
        };
        repositoryMock
            .Setup(ur => ur.Add(It.Is<TaskVisualizerWeb.Domain.Models.User.User>(u => 
                u.Name == userToBeAdded.Name && 
                u.Email == userToBeAdded.Email &&
                u.Status == (TaskVisualizerWeb.Domain.Models.User.UserStatusEnum) userToBeAdded.Status
                )))
            .Returns(response);

        // Act
        var createdUser = service.Add(userToBeAdded);

        // Assert
        createdUser.Should().BeEquivalentTo(userToBeAdded);
    }
}