using FluentValidation;

namespace TaskVisualizerWeb.Domain.Models.User;
public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        // Net4xRegex passes all the tests but AspNetCoreCompatible does not
        RuleFor(u => u.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.Net4xRegex);
        RuleFor(u => u.Name).NotEmpty();
    }
}
