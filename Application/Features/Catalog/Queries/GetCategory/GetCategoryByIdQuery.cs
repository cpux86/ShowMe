using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Catalog.Queries.GetMenu;

namespace Application.Features.Catalog.Queries.GetCategory
{
    public class GetCategoryByIdQuery : IRequest<List<MenuViewModel>>
    {
        public long Id { get; set; }
        public int Level { get; set; }
    }
}
