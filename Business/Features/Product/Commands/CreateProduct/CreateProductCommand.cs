using Business.Wrappers;
using Core.Constants.Enums;
using MediatR;

namespace Business.Features.Product.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Response>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductType Type { get; set; }
    }
}
