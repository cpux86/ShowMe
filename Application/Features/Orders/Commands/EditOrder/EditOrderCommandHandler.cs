using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using Application.Interfaces;
using Domain.Entities.OrderAggregate;
using AutoMapper;

namespace Application.Features.Orders.Commands.EditOrder
{
    public class EditOrderCommandHandler : IRequestHandler<EditOrderCommand, bool>
    {
        private readonly ICatalogContext _dbContext;

        public EditOrderCommandHandler(ICatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(EditOrderCommand request, CancellationToken cancellationToken)
        {
            // Если запрошанный заказ имеется в БД, то получаем его для заполнения
            Order order = _dbContext.Orders.Where(o => o.Id.ToString() == request.OrderId && o.CreatedBy.ToString() == request.CreatedBy).FirstOrDefault();
            if (order==null)
            {
                return false;
            }
            _dbContext.Orders.Attach(order);
            order.Update(request.Title,request.Comments, DateTimeOffset.UtcNow);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
