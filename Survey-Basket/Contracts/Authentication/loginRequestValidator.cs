using FluentValidation;
using Survey_Basket.Contracts.Poll;

namespace Survey_Basket.Contracts.Authentication;

public class loginRequestValidator : AbstractValidator<LoginRequest>
{ 
    public loginRequestValidator()
    {
       
        RuleFor(x=>x.Email).NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}