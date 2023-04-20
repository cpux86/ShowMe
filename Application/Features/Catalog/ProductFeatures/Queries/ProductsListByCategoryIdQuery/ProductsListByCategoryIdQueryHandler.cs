using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace Application.Features.Catalog.ProductFeatures.Queries.ProductsListByCategoryIdQuery
{
    internal class ProductsListByCategoryIdQueryHandler : IRequestHandler<ProductsListByCategoryIdQuery, ProductsDTO>
    {
        private readonly ICatalogContext _catalogContext;

        public ProductsListByCategoryIdQueryHandler(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<ProductsDTO> Handle(ProductsListByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _catalogContext.Products
                .Where(p => p.CategoryId == request.CategoryId)
                .Select(p => new Product { Id = p.Id, Name = p.Title, Price = p.Price })
                .ToListAsync(CancellationToken.None);
            ProductsDTO productsVM = new ProductsDTO { Products = products };
            return productsVM;
        }
    }
}
