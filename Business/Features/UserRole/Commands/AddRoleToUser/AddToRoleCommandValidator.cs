
using FluentValidation;

namespace Business.Features.UserRole.Commands.AddRoleToUser
{
    public class AddToRoleCommandValidator : AbstractValidator<AddRoleToUserCommand>
    {
        public AddToRoleCommandValidator()
        {
            RuleFor(x => x.UserId)
               .NotEmpty().WithMessage("Cannot be empty userid");

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Cannot be empty roleid");
        }
    }
}
