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
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Application.Features.Catalog.Queries.GetMenu
{
    public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, List<Category>>
    {
        private readonly ICatalogContext _catalogContext;
        private readonly IMapper _mapper;

        public GetMenuQueryHandler(ICatalogContext catalogContext, IMapper mapper)
        {
            _catalogContext = catalogContext;
            _mapper = mapper;
        }

        public async Task<List<Category>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {

            // подгружаю все категории меню в контекст, не зависимо от того, целиком или определенную часть меню мы хотим получить.
            // AsNoTracking не использую так как в контекст не загружаются связанные сущности, категории не содержать вложенные категории. 
            //var parent = await _catalogContext
            //    .Categories
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(c => c.Id == 1, CancellationToken.None);

            var allMenuList = await _catalogContext.Categories
                .OrderBy(s => s.Sort)
                .ToListAsync(CancellationToken.None);
            // дописываю к каждой url (slug), id категории
            allMenuList.ForEach(c => c.Slug = $"{c.Slug}-{c.Id}");
            // добавляю к slug категории, slug ее родителя. Должен иметь вит /[slug_parent]/slug_category
            allMenuList.Where(e => e.Parent != null).ToList()
                .ForEach(e => e.Slug = $"{e.Parent.Slug}/{e.Slug}");


            // получаю содержимое категории по id с вложениями на всю глубину.  
            //var root =  allMenuList.Where(c => c.Parent?.Id == 25).ToList();

            //foreach (var item in allMenuList)
            //{
            //    // url вида category_name-category_id/sub_category_name-category_id/
            //    //item.Slug = item.Parent == null ? $"/{item.Slug}-{item.Id}" : $"{item.Parent.Slug}/{item.Slug}-{item.Id}";

            //    item.Slug = $"/{item.Slug}-{item.Id}";


            //    //if (item.Parent != null)
            //    //{
            //    //    item.Slug = $"{item.Parent.Slug}-{item.Parent.Id}/{item.Slug}";
            //    //}

            //}

            // видоизменяю Slug в каждом пункте меню, пока оно имеет плоский вид. После выборки изменить slug таким образом не получилось.
            //allMenuList.ForEach(c => c.Slug = $"/{c.Slug}-{c.Id}");

            // выбираю все меню
            var root = allMenuList
                .Where(c => c.Parent == null).ToList();

            //var category = allMenuList.Where(c=>c.ParentId == 1).ToList();


            // получаем определенную ветвь меню
            //var root = await _catalogContext.Categories.Where(c => c.Parent.Id == 2).ToListAsync(cancellationToken);
            //var root = await _catalogContext.Categories.Where(c => c.Parent.Id == 1 && c.Level <= 1).ToListAsync(CancellationToken.None);

            var menuViewModel = new List<MenuViewModel> { };

            //foreach (var item in root)
            //{
            //    // url вида category_name-category_id/sub_category_name-category_id/
            //    //item.Slug = item.Parent == null ? $"/{item.Slug}-{item.Id}" : $"{item.Parent.Slug}/{item.Slug}-{item.Id}";

            //    //item.Slug = $"/{item.Slug}-{item.Id}";
            //    menuViewModel.Add(_mapper.Map<MenuViewModel>(item));
                
            //}



            //root.ForEach(c=>Console.WriteLine(c.Title));
            
            
            root.ForEach(c => menuViewModel.Add(_mapper.Map<MenuViewModel>(c)));

            //var menuVm = _mapper.Map<MenuVm>(menuViewModele.FirstOrDefault());
            return root;
            //return menuViewModel;
        }
    }
}
