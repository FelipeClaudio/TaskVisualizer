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
    public async System.Threading.Tasks.Task ValidateAndThrowAsync_UserWithInvalidEmail_ShouldThrowException(string email)
    {
        // Arrange
        var user = new Builder().CreateNew<TaskVisualizerWeb.Domain.Models.User.User>()
            .With(u => u.Email = email).Build();

        var validator = new UserValidator();

        // Act
        Func<System.Threading.Tasks.Task> act = () => validator.ValidateAndThrowAsync(user);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
