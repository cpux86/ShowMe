using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Features.Catalog.Queries.GetMenu;

namespace Application.Features.Catalog.Queries.GetCategory
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, List<MenuViewModel>>
    {
        private readonly ICatalogContext _catalogContext;
        private readonly IMapper _mapper;

        public GetCategoryByIdHandler(ICatalogContext catalogContext, IMapper mapper)
        {
            _catalogContext = catalogContext;
            _mapper = mapper;
        }

        public async Task<List<MenuViewModel>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {


            var parent = await _catalogContext
                             .Categories
                             //.AsNoTracking()
                             .FirstOrDefaultAsync(c => c.Id == request.Id, CancellationToken.None) ?? throw new Exception("Категория не найдена");

            var category = await _catalogContext.Categories
                .Where(c => c.LeftKey > parent.LeftKey && c.RightKey < parent.RightKey)

            .ToListAsync(CancellationToken.None);

            var t = category.Where(c => c.ParentId == request.Id).ToList();
            return _mapper.Map<List<MenuViewModel>>(t);
        }
    }
}
