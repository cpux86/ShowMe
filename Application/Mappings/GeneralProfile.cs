using System.Runtime.CompilerServices;
using AutoMapper;
using Domain.Entities.Catalog;
using Domain.Entities.ProductAggregate;
using Application.Features.Catalog.ProductFeatures.Queries.ProductDetailsByIdQuery;
using Application.Features.Catalog.Queries.GetCategory;
using Application.Features.Catalog.Queries.GetMenu;
using Microsoft.EntityFrameworkCore.Design.Internal;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        private string url;

        private string GetUrl(Category category)
        {
            return category.Slug;
        }
        public GeneralProfile()
        {
            AllowNullCollections = true;
            CreateMap<Category, MenuViewModel>()
                //.ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
                //.ForMember(dest => dest.Url, opt => opt.MapFrom(src => "/" + src.Parent.Slug + "-" + src.Id))
                //.ForMember(dest => dest.Url, opt => opt.MapFrom(src => $"/{src.Parent.Slug}-{src.Parent.Id}/{src.Slug}-{src.Id}" ))
                //.ForMember(dest => dest.Url, opt => opt.MapFrom(src =>$"{src.Slug}-{src.Id}"))

                //.ForMember(dest => dest.Url, opt => opt.MapFrom(src => $"/{src.Parent.Slug}-{src.Parent.Id}/{src.Slug}-{src.Id}"))

                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Slug))

                // формирует не полный url, без учета родителей
                //.ForMember(dest => dest.Url, opt
                //    => opt.MapFrom(src => $"/{src.Slug}-{src.Id}"))

                .ForMember(dest => dest.ImageUrl, opt 
                    => opt.MapFrom(src => src.ImageUrl));



            //CreateMap<MenuViewModel, MenuVm>()
            //    .ForMember(dest =>dest.Categories, opt=>opt.MapFrom(src=>src.Categories));

            CreateMap<Category, CategoryViewModel>();
            CreateMap<Product, ProductDetails>();

        }
    }
}
