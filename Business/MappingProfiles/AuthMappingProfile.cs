
using AutoMapper;
using Business.Features.Auth.Commands.AuthRegister;
using Core.Entities;

namespace Business.MappingProfiles
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<AuthRegisterCommand, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
           
        }

    }
}
