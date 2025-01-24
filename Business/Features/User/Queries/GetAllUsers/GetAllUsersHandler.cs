
using AutoMapper;
using Business.Features.User.Dtos;
using Business.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Features.User.Queries.GetAllUsers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Response<List<UserDto>>>
    {
        private readonly UserManager<Core.Entities.User> _userManager;
        private readonly IMapper _mapper;

        public GetAllUsersHandler(UserManager<Core.Entities.User> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<UserDto>>
            {
                Data = _mapper.Map<List<UserDto>>(await _userManager.Users.ToListAsync())
            };
        }
    }
}
