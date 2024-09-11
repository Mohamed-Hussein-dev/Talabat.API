using System.Linq.Expressions;
using System.Security.Cryptography;
using Talabat.Core.Entities;


namespace Talabat.Core.Specifications
{
    public class ProductSpacifications : BaseSapcifications<Product>
    {
        public ProductSpacifications(ProdcutSpecParams Params):
            base(P=>
                    (string.IsNullOrEmpty(Params.Search) || P.Name.ToLower().Contains(Params.Search))
                    &&
                    (!Params.BrandId.HasValue|| P.ProductBrandId == Params.BrandId)
                    &&
                    (!Params.TypeId.HasValue || P.ProductTypeId == Params.TypeId)
                ) 
        {
            if (!string.IsNullOrEmpty(Params.sort))
            {
                switch (Params.sort)
                {
                    case "Price":
                        AddOrder(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderDescending(P => P.Price);
                        break;
                    default:
                        AddOrder(P => P.Name);
                        break;
                }
            }

            
            ApplayPagination((Params.PageIndex - 1) * Params.PageSize, Params.PageSize);
           
        }
        public ProductSpacifications(Expression<Func<Product , bool>> expression) : base(expression){}

        public ProductSpacifications IncludeBrand()
        {
            Includes.Add(Product => Product.ProductBrand);
            return this;
        }

        public ProductSpacifications IncludeType()
        {
            Includes.Add(Product => Product.ProductType);
            return this;
        }

    }
}
