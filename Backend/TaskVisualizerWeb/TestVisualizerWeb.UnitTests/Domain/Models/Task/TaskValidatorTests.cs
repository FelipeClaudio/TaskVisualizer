using FizzWare.NBuilder;
using FluentAssertions;
using FluentValidation;
using Moq;
using TaskVisualizerWeb.Domain;
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

        var validator = new TaskValidator(new DateProvider());

        // Act
        Func<System.Threading.Tasks.Task> act = () => validator.ValidateAndThrowAsync(task);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async System.Threading.Tasks.Task ValidateAndThrowAsync_TaskWithPastDate_ShouldThrowException()
    {
        // Arrange
        var currentDate = new DateTime(2024, 11, 15);
        var dateProviderMock = new Mock<IDateProvider>();
        dateProviderMock.Setup(d => d.Now()).Returns(currentDate);

        var task = new Builder().CreateNew<TaskVisualizerWeb.Domain.Models.Task.Task>()
            .With(t => t.DueDate = currentDate.AddDays(-1)).Build();

        var validator = new TaskValidator(dateProviderMock.Object);

        // Act
        Func<System.Threading.Tasks.Task> act = () => validator.ValidateAndThrowAsync(task);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
