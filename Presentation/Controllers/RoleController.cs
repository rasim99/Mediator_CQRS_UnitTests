
using Business.Features.Role.Dtos;
using Business.Features.Role.Queries.GetAllRoles;
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

    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region Documentation
        /// <summary>
        /// Roles List
        /// </summary>
        
        /// <returns></returns>
        [ProducesResponseType(typeof(Response<List<RoleDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpGet]
        public async Task<Response<List<RoleDto>>>  GetAllRolesAsync()
            => await  _mediator.Send(new GetAllRolesQuery());
    }
}
