using FluentValidation;

namespace TaskVisualizerWeb.Domain.Models.Task;

public class TaskValidator : AbstractValidator<Task>
{
    private readonly IDateProvider _dateProvider;

    public TaskValidator(IDateProvider dateProvider)
    {
        _dateProvider = dateProvider;

        RuleFor(t => (int)t.Points).GreaterThan(0);
        RuleFor(t => t.DueDate).GreaterThan(_dateProvider.Now());
    }
}