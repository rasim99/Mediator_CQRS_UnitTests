using AutoMapper;
using Business.Wrappers;
using Core.Constants.Enums;
using Core.Entities;
using Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;


namespace Business.Features.Auth.Commands.AuthRegister
{
    public class AuthRegisterHandler : IRequestHandler<AuthRegisterCommand, Response>
    {
        private readonly UserManager<Core.Entities.User> _userManager;
        private readonly IMapper _mapper;
        //private readonly IConfiguration _configuration;

        public AuthRegisterHandler(UserManager< Core.Entities.User> userManager, IMapper mapper,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            //_configuration = configuration;
        }

        public async Task<Response> Handle(AuthRegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await new AuthRegisterCommandValidator().ValidateAsync(request);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is not null)
                throw new ValidationException("already exists");
            user = _mapper.Map<Core.Entities.User>(request);
            
            var registerResult = await _userManager.CreateAsync(user, request.Password);
            if (!registerResult.Succeeded)
                throw new ValidationException(registerResult.Errors.Select(x => x.Description));

            var addRoleResult = await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());
            if (!addRoleResult.Succeeded)
                throw new ValidationException(addRoleResult.Errors.Select(x => x.Description));
            return new Response
            {
                Message = "User successfuly created"
            };
        }
    }
}
