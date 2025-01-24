using AutoMapper;
using Business.Features.Product;
using Business.Features.Product.Commands.CreateProduct;
using Business.Features.Product.Commands.UpdateProduct;
using Core.Entities;


namespace Business.MappingProfiles
{
    public class ProductMappingProfile :Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductCommand, Product>();

            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Photo, opt =>
                {
                    opt.Condition(src=>src.Photo is not null);
                    opt.MapFrom(src => src.Photo);
                });

            CreateMap<Product, ProductDto>();

        }
    }
}
