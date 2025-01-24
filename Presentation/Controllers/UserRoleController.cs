using Business.Features.UserRole.Commands.AddRoleToUser;
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
    [Authorize(Roles ="Admin",AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class UserRoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Documentation
        /// <summary>
        /// Add Role To User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Response),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response),StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpPost]
        public async Task<Response> AddRoleToUserAsync(AddRoleToUserCommand request)
            => await _mediator.Send(request);


        //#region Documentation
        ///// <summary>
        ///// Remove Role from User
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        //#endregion

        //[HttpDelete]
        //public async Task<Response> RemoveRoleFromUserAsync(UserRemoveRoleDto model)
        //    => await _userRoleService.RemoveRoleFromUserAsync(model);
    }
}
