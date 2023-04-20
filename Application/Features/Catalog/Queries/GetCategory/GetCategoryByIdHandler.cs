using AutoMapper;
using Domain.Entities.Catalog;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            //await _catalogContext.Categories
            //    .Where(c => c.Id > 1)
            //    .ExecuteDeleteAsync(CancellationToken.None);
            //    .ExecuteUpdateAsync(s => s.SetProperty(c => c.Sort, c => c.Sort-10),CancellationToken.None);


            var cat = new long[] {16,15 };

            var sort = new List<int>();


            var res = await _catalogContext.Categories
                .Where(c => cat.Contains(c.Id) && c.ParentId == request.Id)
                .OrderBy(c=>c.Sort)
                .Select(e=>e.Sort)
                .ToListAsync(CancellationToken.None);

            //for (int i = 0; i < cat.Length; i++)
            //{
            //    var oldSort = res.Find(c => c.Id == cat[i]) ?? throw new BadRequestException();
            //    sort.Add(oldSort.Sort);
            //}

            //int x = 0;
            //foreach (var c in res)
            //{
            //    c.Sort = sort[x];
            //    x++;
            //}


            var t = await _catalogContext.Categories
                .Where(c=>c.ParentId == 1)
                .AllAsync(c => c.ParentId == 1, CancellationToken.None);

            //var category = await _catalogContext.Categories.Include(c => c.Categories).FirstOrDefaultAsync(e => e.Parent.Id == request.Id, cancellationToken);
            var category = await _catalogContext
                .Categories.Where(c => c.ParentId == request.Id)
                .ToListAsync(CancellationToken.None);

            if (category.Count == 0) throw new NotFoundException("не найдена");
           
            

            //// подгружаю все категории меню в контекст, не зависимо от того, целиком или определенную часть меню мы хотим получить.
            //var allMenuList = await _catalogContext.Categories
            //    .OrderBy(s => s.Sort)
            //    .ToListAsync<Category>(cancellationToken);
            //// дописываю к каждой url (slug), id категории
            //allMenuList.ForEach(c => c.Slug = $"{c.Slug}-{c.Id}");
            //// добавляю к slug категории, slug ее родителя. Должен иметь вит /[slug_parent]/slug_category
            //allMenuList.Where(e => e.Parent != null).ToList()
            //    .ForEach(e => e.Slug = $"{e.Parent.Slug}/{e.Slug}");


            //var root = allMenuList.Where(c => c.Parent?.Id == request.Id && c.Level <= request.Level).ToList();






            return _mapper.Map<List<MenuViewModel>>(category);
        }
    }
}
