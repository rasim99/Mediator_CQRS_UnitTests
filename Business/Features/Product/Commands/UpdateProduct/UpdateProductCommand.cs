using Business.Wrappers;
using Core.Constants.Enums;
using MediatR;

namespace Business.Features.Product.Commands.UpdateProduct
{
    public class UpdateProductCommand :IRequest<Response>
    {
        public int Id { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Photo { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public ProductType Type { get; set; }
    }
}
