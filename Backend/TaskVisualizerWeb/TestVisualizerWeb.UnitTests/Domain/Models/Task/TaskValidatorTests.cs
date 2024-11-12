using FizzWare.NBuilder;
using FluentAssertions;
using FluentValidation;
using TaskVisualizerWeb.Domain.Models.Task;

namespace TestVisualizerWeb.UnitTests.Domain.Models.Task;
public sealed class TaskValidatorTests
{
    [Fact]
    public async System.Threading.Tasks.Task ValidateAndThrowAsync_TaskWithInvalidPoints_ShouldThrowException()
    {
        // Arrange
        var task = new Builder().CreateNew<TaskVisualizerWeb.Domain.Models.Task.Task>()
            .With(t => t.Points = 0).Build();

        var validator = new TaskValidator();

        // Act
        Func<System.Threading.Tasks.Task> act = () => validator.ValidateAndThrowAsync(task);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
