using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order
{
    public class Order : BaseEntity
    {
        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.PENDING;

        public Address ShippingAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; } // FK

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public decimal SubTotal { get; set; }

        public decimal GetTotal()=> SubTotal + DeliveryMethod.Cost;

        public string PaymetnIntentid { get; set; }

    }
}
