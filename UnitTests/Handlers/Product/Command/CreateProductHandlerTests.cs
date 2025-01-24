
using AutoMapper;
using Business.Features.Product.Commands.CreateProduct;
using Core.Constants.Enums;
using Core.Exceptions;
using Data.Repositories.Abstract.Product;
using Data.UnitOfWork;
using Moq;
using UnitTests.MockData.Product.CreateProductHandler;

namespace UnitTests.Handlers.Product.Command
{
    public class CreateProductHandlerTests
    {
        private readonly Mock<IProductWriteRepository> _productWriteRepository;
        private readonly Mock<IProductReadRepository> _productReadRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly CreateProductHandler _handler;

        public CreateProductHandlerTests()
        {
             _productWriteRepository = new Mock<IProductWriteRepository>();
             _productReadRepository = new Mock<IProductReadRepository>();
            _mapper = new Mock<IMapper>();
             _unitOfWork = new Mock<IUnitOfWork>();

            _handler = new CreateProductHandler(_productWriteRepository.Object,
                _productReadRepository.Object,_mapper.Object,_unitOfWork.Object);
        }

        [Fact]
        public async Task Handle_WhenValidatorFailed_ShouldThrowValidationException()
        {
            //Arrange
            var request = CreateProductHandlerMockData.CreateProductCommandV1;

            //Act
            Func<Task> func=async () => await _handler.Handle(request,It.IsAny<CancellationToken>());

            //Assert
           var exception=await Assert.ThrowsAsync<ValidationException>(func);
            Assert.Contains("Please enter photo", exception.Errors);
        }

        [Fact]
        public async Task Handle_WhenProductAlreadyExist_ShouldThrowValidationException()
        {
            //Arrange
            var request = CreateProductHandlerMockData.CreateProductCommandV2;

            _productReadRepository.Setup(x => x.GetByNameAsync(request.Name))
                .ReturnsAsync(new Core.Entities.Product());

            //Act
            Func<Task> func = async () => await _handler.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(func);
            Assert.Contains("Already exist with this name", exception.Errors);

        }

    }
}
