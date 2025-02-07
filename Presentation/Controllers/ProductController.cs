using Business.Features.Product;
using Business.Features.Product.Commands.CreateProduct;
using Business.Features.Product.Commands.DeleteProduct;
using Business.Features.Product.Commands.UpdateProduct;
using Business.Features.Product.Queries.GetAllProducts;
using Business.Features.Product.Queries.GetProduct;
using Business.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }


        #region Documentation
        /// <summary>
        /// List of products
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(Response<List<ProductDto>>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response),StatusCodes.Status500InternalServerError)]
        #endregion

        [HttpGet]
        public async Task<Response<List<ProductDto>>> GetAllProductAsync()
         => await _mediator.Send(new GetAllProductsQuery());

        #region Documentation
        /// <summary>
        /// GetProduct
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Response<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response),StatusCodes.Status500InternalServerError)]

        #endregion

        [HttpGet("{id}")]
        public async Task<Response<ProductDto>> GetProductAsync(int id)
            
            => await _mediator.Send( new GetProductQuery { Id=id});

     

        #region Documentation
        /// <summary>
        ///  For creating Product
        /// </summary>
        /// <remarks>
        /// <ul>
        /// <li>  <b>Type:</b> <p> 0 - New 1 - Sold </p>  </li>
        /// </ul>
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Response),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response),StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpPost]
        public async Task<Response> CreateProductAsync(CreateProductCommand request)
            =>await _mediator.Send(request);


        #region Documentation
        /// <summary>
        /// Update of Product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        #endregion

        [HttpPut("{id}")]
        public async Task<Response> UpdateProductAsync(int id, UpdateProductCommand request)
        {
            request.Id = id;
          return  await _mediator.Send(request);
        }

        #region Documentation
        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        #endregion
        [HttpDelete("{id}")]
        public async Task<Response> DeleteProductAsync(int id)
            => await _mediator.Send(new DeleteProductCommand { Id=id});


    }
}
