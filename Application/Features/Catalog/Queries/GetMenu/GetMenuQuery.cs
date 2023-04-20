using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Catalog.Queries.GetMenu
{
    public class GetMenuQuery : IRequest<List<MenuViewModel>>
    {
    }
}
