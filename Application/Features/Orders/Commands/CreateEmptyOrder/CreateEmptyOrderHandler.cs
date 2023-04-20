using Domain.Entities.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.Features.Orders.Commands.CreateEmptyOrder
{
    public class CreateEmptyOrderHandler : IRequestHandler<CreateEmptyOrderCommand, Guid>
    {
        private readonly ICatalogContext _dbContext;

        public CreateEmptyOrderHandler(ICatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateEmptyOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(request.UserId, DateTimeOffset.UtcNow);
            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}
