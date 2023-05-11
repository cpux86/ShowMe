
using Application.Common.Exceptions;
using Application.Features.Catalog.Commands.AddCategory;
using Application.Features.Catalog.Commands.DeleteCategory;
using Application.Features.Catalog.Commands.UpdateCategory;
using Application.Features.Catalog.Queries.GetCategory;
using Application.Features.Catalog.Queries.GetMenu;
using Application.Wrappers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShowMe.WebApi.Modeles.Catalog.Category;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Catalog.Commands.SortCategory;
using Domain.Entities.Catalog;
using WebApi.Modeles;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CategoriesController : BaseApiController
    {
        private readonly IMapper _mapper;

        public CategoriesController(IMapper mapper)
        {
            _mapper = mapper;
        }


        // Получить все категории каталога
        [Route("categories")]
        [HttpGet]
        public async Task<ActionResult<Response<Category>>> GetAllMenu(int? id, int? level)
        {
            var query = new GetMenuQuery();
            var menu = await Mediator.Send(query);
            var vm = new Response<List<Category>>(menu);
            return Ok(vm);
        }

        // GetSubCategory - Листинг категории/получить содержимое категории, по id 
        // Получить категорию по id
        //[Route("{id}")]
        //[Route("categories")]
        //[HttpGet("{id}", Title = "GetCategoryById")]
        [HttpGet]
        [Route("categories/{id}")]
        
        public async Task<ActionResult<Response<CategoryViewModel>>> GetCategoryById(int id, int level)
        {
            var query = new GetCategoryByIdQuery { Id = id, Level = level};
            var result = await Mediator.Send(query);
            var res = new Response<List<MenuViewModel>>(result);
            return Ok(res);
        }


        // Вставить новую категорию
        [Route("categories/add")]
        [HttpPost]
        public async Task<ActionResult<Response<string>>> Create([FromForm] CreateCategoryDto dto)
        {

            if (!ModelState.IsValid)
                throw new BadRequestException();

            var command = new AddCategoryCommand { Name = dto.Name, ParentId = dto.Parent };
            var status = await Mediator.Send(command);
            if (status == null) return new Response<string>();
            var vm = new Response<Category>(status);
            //return CreatedAtRoute("GetCategoryById", new { id = status }, vm);
            return Ok(vm);
        }



        // Удалить категорию
        [Route("category")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteCategoryCommand command)
        {
            //command.Id = id;
            await Mediator.Send(command);
            var vm = new Response<string>(string.Empty, "succeeded");
            return Ok(vm);
        }


        // Обновить категорию
        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult> Update([FromForm] UpdateCategoryDto dto, long id)
        {
            var command = new UpdateCategoryCommand
            {
                Id = id,
                Title = dto.Title
            };
            //command.Id = id;
            await Mediator.Send(command);
            var vm = new Response<string>(string.Empty, "succeeded");
            return Ok(vm);
        }
        [Route("category/sort/")]
        [HttpPost]
        public async Task<ActionResult> Sort([FromForm]SortCategoryCommand command)
        {
            var result = await Mediator.Send(command);
            var data = new Response<List<Category>>(result, "succeeded");
            return Ok(data);
        }

    }
}
