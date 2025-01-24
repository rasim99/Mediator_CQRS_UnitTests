using Business.Wrappers;
using MediatR;

namespace Business.Features.UserRole.Commands.RemoveRoleFromUser
{
    public class RemoveRoleFromUserCommand : IRequest<Response>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
