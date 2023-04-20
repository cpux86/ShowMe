using AutoMapper;
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

namespace Application.Features.Catalog.ProductFeatures.Queries.ProductDetailsByIdQuery
{
    public class ProductDetailsByIdQueryHandler : IRequestHandler<ProductDetailsByIdQuery, ProductDetails>
    {
        private readonly ICatalogContext _catalogContext;
        private readonly IMapper _mapper;

        public ProductDetailsByIdQueryHandler(ICatalogContext catalogContext, IMapper mapper)
        {
            _catalogContext = catalogContext;
            _mapper = mapper;
        }

        public async Task<ProductDetails> Handle(ProductDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            Product product = await _catalogContext.Products
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync() ?? throw new BadRequestException("bad request");

            return _mapper.Map<ProductDetails>(product);
        }
    }
}
