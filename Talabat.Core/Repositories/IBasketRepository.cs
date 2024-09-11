using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositories
{
    public interface IBasketRepository
    {
        //get bustket need => basketId , return basket
        Task<CustomerBasket?> GetBasketAsync(string BasketId);

        //update or creat basket need => basketId , return basket
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket);
        // delete basket
        Task<bool> DeleteBasketAsync(string BasketId);
    }
}
