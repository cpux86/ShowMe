//using Application.Features.Catalog.ProductFeatures.Commandes.AddProduct;
//using Application.Features.Catalog.ProductFeatures.Commandes.DeleteProductById;
//using Application.Features.Catalog.ProductFeatures.Commandes.UpdateProduct;
//using Application.Features.Catalog.ProductFeatures.Queries.ProductDetailsByIdQuery;
//using Application.Features.Catalog.ProductFeatures.Queries.ProductsListByCategoryIdQuery;
//using AutoMapper;
//using Domain.Entities.ProductAggregate;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Application.Common.Exceptions;
//using Application.Wrappers;
//using System.Threading.Tasks;
//using WebApi.Modeles;

//namespace WebApi.Controllers.v1
//{
//    [ApiVersion("1.0")]
//    public class ProductsController : BaseApiController
//    {
//        private readonly IMapper _mapper;

//        public ProductsController(IMapper mapper)
//        {
//            _mapper = mapper;
//        }

//        [HttpPost]
//        public async Task<ActionResult<Response<string>>> Create([FromForm] ProductRequest product)
//        {
//            var command = _mapper.Map<AddProductCommand>(product);
//            var status = await Mediator.Send(command);
//            return Ok(status);
//        }

//        [Route("{id}")]
//        [HttpGet]
//        public async Task<Response<ProductDetailsDTO>> Details(int id)
//        {
//            var command = new ProductDetailsByIdQuery { Id = id };
//            var result = await Mediator.Send(command);
//            //return new Response<ProductDetails>(result);
//            ProductDetailsDTO product = new ProductDetailsDTO { Product = result };
//            return new Response<ProductDetailsDTO>(product);
//        }

//        [Route("{id:long}")]
//        [HttpDelete]
//        public async Task<ActionResult<Response<string>>> DeleteById(int id)
//        {
//            var command = new DeleteProductByIdCommand { Id = id };
//            await Mediator.Send(command);
//            return new Response<string>("", "succeed");
//        }

//        [HttpGet]
//        public async Task<Response<ProductsDTO>> GetProductsFromCategory([FromQuery] int category)
//        {
//            var query = new ProductsListByCategoryIdQuery() { CategoryId = category };
//            ProductsDTO result = await Mediator.Send(query);
  
//            return new Response<ProductsDTO>(result);
//        }

//        //[Route("{id:long}")]
//        //[HttpPut]
//        //public async Task<ActionResult<Response<string>>> Update([FromForm] ProductRequest product, long id)
//        //{
//        //    var command = new UpdateProductCommand() {
//        //        Title = product.Title,
//        //        Description = product.Description,
//        //        Price = product.Price,
//        //        Quantity = product.Quantity,
//        //        ProductId = id
//        //    };
//        //    var status = await Mediator.Send(command);
//        //    return Ok(status);
//        //}

//    }
//}
