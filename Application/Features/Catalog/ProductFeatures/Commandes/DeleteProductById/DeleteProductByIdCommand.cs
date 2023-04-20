using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Catalog.ProductFeatures.Commandes.DeleteProductById
{
    public class DeleteProductByIdCommand : IRequest
    {
        public long Id { get; set; }    
    }
}
