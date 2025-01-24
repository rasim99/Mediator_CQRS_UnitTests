
using Business.Features.Auth.Commands.AuthLogin;
using Business.Features.Auth.Commands.AuthRegister;
using Business.Features.Auth.Dtos;
using Business.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Documentation
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Response),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response),StatusCodes.Status500InternalServerError)]
        #endregion

        [HttpPost("register")]
        public async Task<Response> RegisterAsync(AuthRegisterCommand request)
            => await _mediator.Send(request);


        #region Documentation
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Response<LoginDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpPost("login")]

        public async Task<Response<LoginDto>> LoginAsync(AuthLoginCommand model)
            => await _mediator.Send(model);


    }

}
