using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Catalog.Queries.GetMenu;
using Domain.Entities.Catalog;

namespace Application.Features.Catalog.Queries.GetCategory
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Путь к файлу изображения категории
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// последняя секция URL
        /// </summary>
        public string Slug { get; set; }
        public IEnumerable<MenuViewModel> Categories { get; set; }
    }
}
