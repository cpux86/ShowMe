using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Catalog.ProductFeatures.Queries.ProductDetailsByIdQuery
{
    public class ProductDetailsByIdQuery : IRequest<ProductDetails>
    {
        public int Id { get; set; }
    }
}
