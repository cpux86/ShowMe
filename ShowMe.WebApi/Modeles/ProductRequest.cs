using Application.Features.Catalog.ProductFeatures.Commandes.AddProduct;
using AutoMapper;

namespace WebApi.Modeles
{
    public class ProductRequest : Profile
    {
        public ProductRequest()
        {
            CreateMap<ProductRequest, AddProductCommand>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        }

        public string Name { get; set; }
        public string Description { get; set; } 
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Category { get; set; }
    }
}
