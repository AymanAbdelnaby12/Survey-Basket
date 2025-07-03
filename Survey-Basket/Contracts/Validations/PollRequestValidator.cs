using FluentValidation;
using SurveyBasket.Contracts.Requests;

namespace SurveyBasket.Contracts.Validations;

public class PollRequestValidator : AbstractValidator<PollRequest>
{ 
    public PollRequestValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");
        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        RuleFor(p => p.StartsAt)
            .LessThanOrEqualTo(p => p.EndsAt).WithMessage("Start date must be before or equal to end date.");
        RuleFor(p => p.EndsAt)
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("End date must be in the future.");

    }
}