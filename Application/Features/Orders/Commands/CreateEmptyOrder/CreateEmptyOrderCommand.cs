using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.CreateEmptyOrder
{
    public class CreateEmptyOrderCommand : IRequest<Guid>
    {
        public string UserId { get; set; }
    }
}
