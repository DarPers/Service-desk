using FluentValidation;
using ServiceDeskAPI.ViewModels;

namespace ServiceDeskAPI.Validators;

public class UserUpdatingModelValidator : AbstractValidator<UserUpdatingViewModel>
{
    public UserUpdatingModelValidator()
    {
        RuleFor(user => user.Email).NotNull().EmailAddress();
        RuleFor(user => user.FirstName).NotNull().MinimumLength(5).MaximumLength(50);
        RuleFor(user => user.FirstName).NotNull().MinimumLength(5).MaximumLength(50);
    }
}
