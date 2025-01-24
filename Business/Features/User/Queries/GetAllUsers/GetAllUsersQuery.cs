
using Business.Features.User.Dtos;
using Business.Wrappers;
using MediatR;

namespace Business.Features.User.Queries.GetAllUsers
{
    public class GetAllUsersQuery :IRequest<Response<List<UserDto>>>
    {

    }
}
