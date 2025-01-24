
using Core.Constants.Enums;

namespace Business.Features.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public ProductType Type { get; set; }
    }
}
