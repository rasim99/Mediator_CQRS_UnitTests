using Business.Wrappers;
using MediatR;

namespace Business.Features.UserRole.Commands.AddRoleToUser
{
    public class AddRoleToUserCommand : IRequest<Response>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
