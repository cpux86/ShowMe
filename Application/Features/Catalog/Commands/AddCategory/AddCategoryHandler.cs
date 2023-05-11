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
using FluentValidation.Results;

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

            var sort = await _catalogContext.Categories.MaxAsync(c => (int?) c.Sort, CancellationToken.None) ?? 0;
            subCategory.Sort = sort + 1;

            if (request.ParentId == 0)
            {
                var exists = await _catalogContext.Categories
                    .AnyAsync(p => p.Parent == null && p.Title == request.Name, cancellationToken);
                if (exists) throw new Exception("Конфликт имен");

                var maxRightKey =
                    await _catalogContext.Categories.MaxAsync(k => (int?)k.RightKey, CancellationToken.None) ?? 0;

                subCategory.LeftKey = maxRightKey + 1;
                subCategory.RightKey = subCategory.LeftKey + 1;
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
                         throw new Exception("Категория не найдена"); ;

            var lk = parent.RightKey;
            var rk = lk + 1;

            // подготовить место
            // обновляем правые ключи
            await _catalogContext.Categories
                .Where(k => k.RightKey >= lk)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.RightKey, c => c.RightKey + 2), CancellationToken.None);

            // обновляем левые ключи
            await _catalogContext.Categories
                .Where(k => k.LeftKey >= lk)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.LeftKey, c => c.LeftKey + 2), CancellationToken.None);

            subCategory.LeftKey = lk;
            subCategory.RightKey = rk;
            parent.Add(subCategory);

            await _catalogContext.SaveChangesAsync(cancellationToken);
            return subCategory;
        }
    }
}
