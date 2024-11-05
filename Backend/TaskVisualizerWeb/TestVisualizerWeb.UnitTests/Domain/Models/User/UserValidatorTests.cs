using FizzWare.NBuilder;
using FluentAssertions;
using FluentValidation;
using TaskVisualizerWeb.Domain.Models.User;

namespace TestVisualizerWeb.UnitTests.Domain.Models.User;

public sealed class UserValidatorTests
{
    [InlineData("test@test")]
    [InlineData("test@")]
    [InlineData("test")]
    [InlineData("")]
    [Theory]
    public async Task User_InvalidEmail_ShouldThrowException(string email)
    {
        // Arrange
        var user = new Builder().CreateNew<TaskVisualizerWeb.Domain.Models.User.User>()
            .With(u => u.Email = email).Build();

        var validator = new UserValidator();

        // Act
        Func<Task> act = () => validator.ValidateAndThrowAsync(user);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task User_EmptyName_ShouldThrowException()
    {
        // Arrange
        var user = new Builder().CreateNew<TaskVisualizerWeb.Domain.Models.User.User>()
            .With(u => u.Name = string.Empty).Build();

        var validator = new UserValidator();

        // Act
        Func<Task> act = () => validator.ValidateAndThrowAsync(user);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
