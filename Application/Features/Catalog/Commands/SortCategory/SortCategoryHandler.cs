using Application.Interfaces;
using Domain.Entities.Catalog;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Catalog.Commands.SortCategory
{
    internal class SortCategoryHandler : IRequestHandler<SortCategoryCommand, List<Category>>
    {
        private readonly ICatalogContext _catalogContext;

        public SortCategoryHandler(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }
        public async Task<List<Category>> Handle(SortCategoryCommand request, CancellationToken cancellationToken)
        {
            // получаем содержимое категории
            var categoryList = await _catalogContext.Categories
                .Where(c => c.ParentId == request.CategoryId)
                .OrderBy(s => s.Sort)
                .AsNoTracking()
                .ToListAsync(CancellationToken.None);
            // извлекаем порядок


            //int[] sort = new int[categoryList.Count];

            var s = categoryList.Select(e => e.Sort).ToList();

            var count = 0;
            foreach (var category in categoryList)
            {
                categoryList.Find(e => e.Id == request.Sort[count]).Sort = s[count];
                count++;
            }

            //_catalogContext.Categories.UpdateRange(categoryList);

            //await _catalogContext.SaveChangesAsync(CancellationToken.None);

            //var res = await _catalogContext.Categories
            //    .Where(c => request.Sort.Contains(c.Id) && c.ParentId == request.CategoryId)
            //    //.OrderBy(c => c.Sort)
            //    //.Select(e => e.Sort)
            //    .ToListAsync(CancellationToken.None);

            await _catalogContext.Categories.BulkUpdateAsync(categoryList, CancellationToken.None);
            return categoryList.OrderBy(s=>s.Sort).ToList();
        }
    }
}
