using Application.Common.Exceptions;
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

namespace Application.Features.Catalog.Commands.AddCategory
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, Category>
    {
        private readonly ICatalogContext _catalogContext;

        public AddCategoryHandler(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<Category> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            // создаем новую категорию
            var subCategory = new Category(request.Name);

            var sort = await _catalogContext.Categories.MaxAsync(c => (int?)c.Sort, CancellationToken.None) ?? 0;
            subCategory.Sort = sort + 1;

            if (request.ParentId == 0)
            {
                var exists = await _catalogContext.Categories
                    .AnyAsync(p => p.Parent == null && p.Title == request.Name, cancellationToken);
                if (exists) throw new Exception("Конфликт имен");
                _catalogContext.Categories.Add(subCategory);
                

                await _catalogContext.SaveChangesAsync(cancellationToken);
                return subCategory;
            }

            // создаем подкатегорию
            // получаем категорию в которую необходимо вставить подкатегорию
            var parent = await _catalogContext.Categories
                             // подгружаем ее дочерние категории
                             .Include(c => c.Categories)
                             ?.FirstOrDefaultAsync(c => c.Id == request.ParentId, cancellationToken) ??
                         throw new Exception("Категория не найдена");
            //if (parent == null) return subCategory;

            parent.Add(subCategory);

            
            await _catalogContext.SaveChangesAsync(cancellationToken);
            return subCategory;
        }
    }
}
