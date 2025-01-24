
using FluentValidation;

namespace Business.Features.Auth.Commands.AuthRegister
{
    public class AuthRegisterCommandValidator :AbstractValidator<AuthRegisterCommand>
    {
        public AuthRegisterCommandValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Please enter email")
           .EmailAddress().WithMessage("Incorrect email format!");

            RuleFor(x => x.Password.Length)
               .NotEmpty().WithMessage("Please enter password")
                .GreaterThanOrEqualTo(8)
                .WithMessage("Password lenght must be minimum 8 character");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Please enter confirmation password")
                .Equal(x => x.ConfirmPassword)
                .WithMessage("passwords are not equal !");
        }
    }
}
