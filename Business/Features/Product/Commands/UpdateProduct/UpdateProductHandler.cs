
using AutoMapper;
using Business.Services.Producer;
using Business.Wrappers;
using Core.Exceptions;
using Data.Repositories.Abstract.Product;
using Data.UnitOfWork;
using MediatR;

namespace Business.Features.Product.Commands.UpdateProduct
{
    internal class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Response>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProducerService _producerService;

        public UpdateProductHandler(IProductReadRepository productReadRepository,
              IProductWriteRepository productWriteRepository,
                IMapper  mapper,
                IUnitOfWork unitOfWork,
                IProducerService producerService)
        {
           _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _producerService = producerService;
        }
        public async Task<Response> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetAsync(request.Id);
            if (product is null)
                throw new NotFoundException("not found product");

            var result = await new UpdateProductCommandValidator().ValidateAsync(request);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
            _mapper.Map(request, product);
            _productWriteRepository.Update(product);
            await _unitOfWork.CommitAsync();
            await _producerService.ProduceAsync("update", product);

            return new Response
            {
                Message = "Successfuly! Product was updated"
            };
        }
    }
}
