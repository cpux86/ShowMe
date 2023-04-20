using Domain.Entities.Catalog;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Catalog.Commands.SortCategory
{
    public class SortCategoryCommand : IRequest<List<Category>>
    {
        /// <summary>
        /// Категория в которой производится изменение порядка следования вложенных категория 
        /// </summary>
        public long CategoryId { get; set; }
        public int[]  Sort { get; set; }
    }
}
