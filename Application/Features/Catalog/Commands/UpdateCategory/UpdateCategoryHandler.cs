using Domain.Entities.Catalog;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.Features.Catalog.Commands.UpdateCategory
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly ICatalogContext _catalogContext;

        public UpdateCategoryHandler(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            // получаю категорию для обновления 
            var updateCategory = _catalogContext.Categories
                .Where(c => c.Id == request.Id)
                .Include(p=>p.Parent)
                .FirstOrDefault();
            if (updateCategory == null) throw new Exception("Category not found");
            // проверяю, будет ли конфликтовать переименованная категория с другими категориями
            var exist = _catalogContext.Categories
                .Where(c => c.Parent == updateCategory.Parent && c.Id != request.Id)
                .Any(e => e.Title == request.Title);

            if (exist) throw new Exception("Конфликт имен");

            
            updateCategory.Update(request.Title);




            await _catalogContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
    