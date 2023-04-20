using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public enum OrderStatus
    {
        Disabled,
        Deleted,
        Active,    
        Locked,
        Moderation
    }
}
