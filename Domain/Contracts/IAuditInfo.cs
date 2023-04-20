using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IAuditInfo
    {
        /// <summary>
        /// Дата создания сущности
        /// </summary>
        DateTimeOffset CreatedOn { get; set; }
        /// <summary>
        /// Владелец сущности(ID юзера)
        /// </summary>
        Guid CreatedBy { get; set; }
        /// <summary>
        /// Дата изменения сущности
        /// </summary>
        DateTimeOffset? ModifiedOn { get; set; }
        /// <summary>
        /// Дата публикации заказа
        /// </summary>
        DateTimeOffset? PublishedOn { get; set; }
        /// <summary>
        /// опубликован ли заказ
        /// </summary>
        public bool IsActive { get; set; }
    }
}
