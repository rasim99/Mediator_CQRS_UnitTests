
using AutoMapper;
using Business.Wrappers;
using Core.Exceptions;
using Data.Repositories.Abstract.Product;
using MediatR;

namespace Business.Features.Product.Queries.GetProduct
{
    public class GetProductHandler : IRequestHandler<GetProductQuery, Response<ProductDto>>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;

        public GetProductHandler(IProductReadRepository productReadRepository,
            IMapper mapper)
        {
            _productReadRepository = productReadRepository;
           _mapper = mapper;
        }
        public async Task<Response<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetAsync(request.Id);
            if (product is null)
                throw new NotFoundException("Not found product");
            return new Response<ProductDto>
            {
                Data = _mapper.Map<ProductDto>(product)
            };
        }
    }
}
