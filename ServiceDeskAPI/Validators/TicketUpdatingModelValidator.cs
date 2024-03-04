using FluentValidation;
using ServiceDeskAPI.ViewModels;

namespace ServiceDeskAPI.Validators;

public class TicketUpdatingModelValidator : AbstractValidator<TicketUpdatingViewModel>
{
    public TicketUpdatingModelValidator()
    {
        RuleFor(ticket => ticket.Name).NotNull().MaximumLength(50);
        RuleFor(ticket => ticket.Description).NotNull().MaximumLength(150);
        RuleFor(ticket => ticket.Status).IsInEnum();
    }
}
