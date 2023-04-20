using Domain.Common;
using Domain.Entities.ProductAggregate;
using System;
using System.Collections.Generic;

namespace Domain.Entities.OrderAggregate
{
    public class Order : BaseDeletableEntity
    {
        private Order() { } // нужен для EF
        public Order(string userID, DateTimeOffset dateTime)
        {
            // назначаем владельца сущьности
            CreatedBy = Guid.Parse(userID);
            // устанавливаем дату создания заказа, дата остается не изменна, не зависимо от статуса заказа
            CreatedOn = dateTime;
            ModifiedOn = dateTime;
        }

        ///// <summary>
        ///// Обновить дату публикации
        ///// </summary>
        ///// <param name="title"></param>
        ///// <param name="comments"></param>
        //public void UpdatePublishedOn(string title, string comments)
        //{
        //    Title = title;
        //    Comments = comments;
        //}

        public Order Update(string title, string comments, DateTimeOffset dateTime)
        {
            Title = title;
            Comments = comments;
            ModifiedOn = dateTime;
            return this;
        }


        //public Guid Id { get; private set; }
        /// <summary>
        /// Заголовок, к примеру "Микросхема ШИМ-контроллера"
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Комментарии, к промеру "Шим-контроллер с частоного преобразователя Siemens"
        /// </summary>
        public string Comments { get; private set; }
        /// <summary>
        /// URL-адреса изображений искомых товаров
        /// </summary>
        public string ImageSource { get; private set; }
        /// <summary>
        /// товары предложенные продовцамаи, подходящие по описанию.
        /// </summary>
        public List<Product> Products { get; set; }
        /// <summary>
        /// Статус заказа: активен, не активен и т.д
        /// </summary>
        public OrderStatus Status { get; private set; }

    }
}
