using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered() { }
        public ProductItemOrdered(int productId, string productName, string pictureUrl)
        {
            this.productId = productId;
            this.productName = productName;
            PictureUrl = pictureUrl;
        }

        public int productId { get; set; }
        public string productName { get; set; }
        public string PictureUrl { get; set; }
    }
}
