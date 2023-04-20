using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IDeletableEntity
    {
        /// <summary>
        /// Удалена сущность
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Дата удаления сущности
        /// </summary>
        public DateTime? DeletedOn { get; set; }
    }
}
