using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;

namespace Talabat.Core.Entities
{
    public class OrderItem : BaseEntity
    {
       public ProductItemOrdered product { get; set; }
       public decimal price { get; set; }
       public int quantity { get; set; }
    }
}
