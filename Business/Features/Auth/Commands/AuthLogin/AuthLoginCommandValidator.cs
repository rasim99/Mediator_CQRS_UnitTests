
using FluentValidation;
using MediatR;

namespace Business.Features.Auth.Commands.AuthLogin
{
    public class AuthLoginCommandValidator :AbstractValidator<AuthLoginCommand>
    {
        public AuthLoginCommandValidator()
        {
            RuleFor(x => x.Email)
             .NotEmpty().WithMessage("Please enter email")
         .EmailAddress().WithMessage("Incorrect email format!");

            RuleFor(x => x.Password.Length)
               .NotEmpty().WithMessage("Please enter password")
                .GreaterThanOrEqualTo(8)
                .WithMessage("Password lenght must be minimum 8 character");
        }
    }
}
