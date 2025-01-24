
using AutoMapper;
using Business.Wrappers;
using Data.Repositories.Abstract.Product;
using MediatR;

namespace Business.Features.Product.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Response<List<ProductDto>>>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IProductReadRepository productReadRepository,IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }
        public async Task<Response<List<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return new Response<List<ProductDto>>
            {
                Data = _mapper.Map<List<ProductDto>>(await _productReadRepository.GetAllAsync()),
            };
        }
    }
}
