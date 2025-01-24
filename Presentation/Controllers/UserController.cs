using AutoMapper;
using Business.Features.User.Dtos;
using Business.Features.User.Queries.GetAllUsers;
using Business.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        #region Documentation
        /// <summary>
        /// Users List
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(Response<List<UserDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpGet]
        public async Task<Response<List<UserDto>>> GetAllUserAsync()
            => await _mediator.Send(new GetAllUsersQuery());
    }
}
