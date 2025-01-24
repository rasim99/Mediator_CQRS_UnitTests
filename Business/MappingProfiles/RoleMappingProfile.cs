using AutoMapper;
using Business.Features.Role.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Business.MappingProfiles
{
    public class RoleMappingProfile :Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<IdentityRole,RoleDto>();
        }
    }
}
