using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;


namespace Talabat.Core.Specifications
{
    public class ProductFilterationCountAsync : BaseSapcifications<Product>
    {
        public ProductFilterationCountAsync(ProdcutSpecParams Params) 
        :base(P =>
            (string.IsNullOrEmpty(Params.Search) || P.Name.Contains(Params.Search))
            &&
            (!Params.BrandId.HasValue || P.ProductBrandId == Params.BrandId)
            &&
            (!Params.TypeId.HasValue || P.ProductTypeId == Params.TypeId)
            )
        {
         
        }
    }
}
