using Application.Features.Catalog.Commands.AddCategory;
using AutoMapper;

namespace WebApi.Modeles
{
    public class CreateCategoryDto : Profile
    {
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Назначение. Родительская категория
        /// </summary>
        //public long ParentId { get; set; }
        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<CreateCategoryDto, AddCategoryCommand>();
        //}
    }
}
