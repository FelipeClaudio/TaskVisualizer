using FluentValidation;

namespace TaskVisualizerWeb.Domain.Models.Task;

public class TaskValidator : AbstractValidator<Task>
{
    public TaskValidator()
    {
        RuleFor(t => (int)t.Points).GreaterThan(0);
    }
}