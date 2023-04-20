using Domain.Entities.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Catalog.ProductFeatures.Commandes.UpdateProduct
{
    public class UpdateProductHandler : IRequest
    {
        private readonly ICatalogContext _catalogContext;

        public UpdateProductHandler(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _catalogContext.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId);
            if (product == null) throw new BadRequestException("Продукт не найден");
            product.Title = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            //Product product = new()
            //{
            //    Id = request.ProductId,
            //    Title = request.Title,
            //    Description = request.Description,
            //    Price = request.Price,
            //    Quantity = request.Quantity
            //    CategoryId = 1
            //};
            //_catalogContext.Products.Update(product);
            await _catalogContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
