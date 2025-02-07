using AutoMapper;
using Business.Services.Producer;
using Business.Wrappers;
using Core.Exceptions;
using Data.Repositories.Abstract;
using Data.Repositories.Abstract.Product;
using Data.UnitOfWork;
using MediatR;


namespace Business.Features.Product.Commands.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Response>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProducerService _producerService;

        public CreateProductHandler(IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IProducerService producerService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _producerService = producerService;
        }

        public async Task<Response> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await new CreateProductCommandValidator().ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var product = await _productReadRepository.GetByNameAsync(request.Name);
            if (product is not null)
                throw new ValidationException("Already exist with this name");
            product = _mapper.Map<Core.Entities.Product>(request);

            await _productWriteRepository.CreateAsync(product);
            await _unitOfWork.CommitAsync();
            await _producerService.ProduceAsync("create",product);
            return new Response
            {
                Message = "Creating successful!"
            };
        }
    }
}
