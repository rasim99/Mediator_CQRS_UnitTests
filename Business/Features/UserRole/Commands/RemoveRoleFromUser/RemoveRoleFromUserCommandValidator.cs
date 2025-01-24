using FluentValidation;

namespace Business.Features.UserRole.Commands.RemoveRoleFromUser
{
    public class RemoveRoleFromUserCommandValidator :AbstractValidator<RemoveRoleFromUserCommand>
    {
        public RemoveRoleFromUserCommandValidator()
        {
            RuleFor(x => x.UserId)
             .NotEmpty().WithMessage("Cannot be empty userid");

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("Cannot be empty roleid");
        }
    }
}
