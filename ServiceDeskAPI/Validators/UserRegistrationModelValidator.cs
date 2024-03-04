using FluentValidation;
using ServiceDeskAPI.ViewModels;

namespace ServiceDeskAPI.Validators;

public class UserRegistrationModelValidator : AbstractValidator<UserRegistrationViewModel>
{
    public UserRegistrationModelValidator()
    {
        RuleFor(user => user.Email).NotNull().EmailAddress();
        RuleFor(user => user.FirstName).NotNull().MinimumLength(5).MaximumLength(50);
        RuleFor(user => user.FirstName).NotNull().MinimumLength(5).MaximumLength(50);
        RuleFor(user => user.Password).NotNull().MinimumLength(8).MaximumLength(25);
        RuleFor(user => user.Role).IsInEnum();
    }
}
