using MediatR;

namespace Application.Features.Catalog.ProductFeatures.Queries.ProductsListByCategoryIdQuery
{
    public class ProductsListByCategoryIdQuery : IRequest<ProductsDTO>
    {
        public int CategoryId { get; set; }
    }
}
