using Ardalis.GuardClauses;
using Domain.Common;
using Domain.Entities.OrderAggregate;
using Domain.Entities.ProductAggregate;
using Domain.Utils;
using NickBuhro.Translit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using Domain.SEO;
using JetBrains.Annotations;

namespace Domain.Entities.Catalog
{
    public class Category
    {
        // Максимальная глубина вложенности меню
        private const int MAX_DEPTH = 5;
        //private const int MAX_DEPH = 5;
        public Category()
        {
            // Требуется для EF
        }

        public Category(string title)
        {
            Title = title;
            // создаю slug для новой категории из ее имени
            Slug = SlugGenerator.ToUrlSlug(title);
        }

        #region Fields
        public int Id { get; set; }
        //[NotNull]
        public string Title { get; private set; } = null!;

        /// <summary>
        /// Путь к файлу изображения категории
        /// </summary>
        [CanBeNull]
        public string ImageUrl { get; private set; } = null;
        /// <summary>
        /// последняя секция URL
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Глубина вложенности меню. Нужно для контроля глубины вложенности меню. Может уберу со временем!!!!!
        /// </summary>
        public int Level { get;  set; }
        /// <summary>
        /// Активна ли категория!
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Порядок сортировки
        /// </summary>
        public int Sort { get; set; }
        public int? ParentId { get; set; }
        /// <summary>
        /// Родительская категория
        /// </summary>
        public virtual Category Parent { get; private set; }

        // дочерние категории
        public virtual List<Category> Categories { get; private set; }
        // Заказы. Запрос на приобретение товара, содержащий описание товара
        public virtual List<Order> Orders { get; private set; }
        // товары которые уже имеются в категории
        public virtual List<Product> Products { get; private set; }

        #region SEO
        public Meta Meta { get; set; }


        #endregion

        #endregion

        #region CRUD v1
        /// <summary>
        /// Добавить подкатегорию
        /// </summary>
        /// <param name="children">Новая категория</param>
        public void Add(Category children)
        {
            // проверка на конфликт имен категорий. не допускаем наличии категорий с одинаковым именем
            if (Categories.Any(c => c.Title.Equals(children.Title.Trim(), StringComparison.CurrentCultureIgnoreCase))) throw new Exception("Конфликт имен");

            // вычисляю значение глубины для новой категории
            var level = Level + 1;
            // проверяю, не достигла новая категория максимальной глубины
            if (level > MAX_DEPTH) throw new Exception("не допустимая глубина вложения");
            // устанавливаю значение глубины для новой категории
            children.Level = level;

            Categories.Add(children);
        }

        public void Update(string name)
        {
            this.Title = name;

            this.Slug = SlugGenerator.ToUrlSlug(name);
        }
        #endregion

        /// <summary>
        /// Добавить продукт в категорию
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
    }
}
