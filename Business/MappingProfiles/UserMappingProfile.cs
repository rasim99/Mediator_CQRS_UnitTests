
using AutoMapper;
using Business.Features.User.Dtos;
using Core.Entities;

namespace Business.MappingProfiles
{
    public class UserMappingProfile :Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User,UserDto>();
        }
    }
}
