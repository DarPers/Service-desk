using FluentValidation;
using ServiceDeskAPI.ViewModels;

namespace ServiceDeskAPI.Validators;

public class TicketCreationModelValidator : AbstractValidator<TicketCreationViewModel>
{
    public TicketCreationModelValidator()
    {
        RuleFor(ticket => ticket.Name).NotNull().MaximumLength(50);
        RuleFor(ticket => ticket.Description).NotNull().MaximumLength(150);
    }
}
