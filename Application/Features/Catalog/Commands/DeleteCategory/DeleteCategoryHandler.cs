using Domain.Entities.Catalog;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Interfaces;

namespace Application.Features.Catalog.Commands.DeleteCategory
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICatalogContext _catalogContext;

        public DeleteCategoryHandler(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {

            // получаю категорию для удаления
            // если запрошенной категории не существует, то выбрасываем исключение
            var category = await _catalogContext.Categories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken) ?? throw new NotFoundException("категория не найдена");
            // проверяю категорию на пустоту
            var isNotEmpty = await _catalogContext.Categories.AnyAsync(ch => ch.Parent.Id == request.Id, cancellationToken);
            // если удаляемая категория содержит вложенные категории (не пуста).
            // если request.Items = all, то согласно запросу разрешаем удалять категории с ее содержимым
            if (!request.Force && isNotEmpty) throw new Exception("Категория не пуста");

            // вычисляем коэффициент выравнивания ключей.
            // вычисляется: (правый ключ уд.кат + 1 - левый ключ уд.кат)
            var index = (category.RightKey + 1 - category.LeftKey);

            // обновляю правые ключи 
            await _catalogContext.Categories
                .Where(k => k.RightKey > category.RightKey)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.RightKey, c => c.RightKey - index), CancellationToken.None);

            // обновляем левые ключи
            await _catalogContext.Categories
                .Where(k => k.LeftKey > category.RightKey)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.LeftKey, c => c.LeftKey - index), CancellationToken.None);

            try
            {
                
                
                _catalogContext.Categories.Remove(category);
                await _catalogContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw new Exception("unidentified");
            }
            return Unit.Value;
                      
        }

    }
}
