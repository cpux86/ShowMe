using Domain.Entities.Catalog;
using Domain.Entities.ProductAggregate;
using MediatR;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalog.ProductFeatures.Commandes.AddProduct
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, long>
    {
        private readonly ICatalogContext _catalogContext;

        public AddProductHandler(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<long> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            Product product = new Product(request.Name);
            product.CategoryId = request.CategoryId;
            product.Price = request.Price;
            product.Description = request.Description;
            _catalogContext.Products.Add(product);
            await _catalogContext.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
}
