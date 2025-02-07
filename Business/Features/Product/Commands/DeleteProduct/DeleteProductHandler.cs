using Business.Services.Producer;
using Business.Wrappers;
using Core.Exceptions;
using Data.Repositories.Abstract.Product;
using Data.UnitOfWork;
using MediatR;

namespace Business.Features.Product.Commands.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Response>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProducerService _producerService;

        public DeleteProductHandler(IProductReadRepository productReadRepository,
              IProductWriteRepository productWriteRepository,
                IUnitOfWork unitOfWork,
                IProducerService producerService)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _unitOfWork = unitOfWork;
           _producerService = producerService;
        }
        public async  Task<Response> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetAsync(request.Id);
            if (product is null)
                throw new NotFoundException("Product is not found");
            _productWriteRepository.Delete(product);
            await _unitOfWork.CommitAsync();
            await _producerService.ProduceAsync("delete", product);

            return new Response
            {
                Message = " Successfull! Product was deleted"
            };
        }
    }
}
