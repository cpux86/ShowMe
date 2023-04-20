using Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public abstract class BaseEntity : IAuditInfo
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        /// <summary>
        /// Дата публикации заказа
        /// </summary>
        public DateTimeOffset? PublishedOn { get; set; }
        /// <summary>
        /// Дата поледней публикации
        /// </summary>
        public DateTimeOffset? LastPublicationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
