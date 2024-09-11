using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers
{

    public class BasketsController : APIBaseController
    {
        private readonly IBasketRepository _basketRepo;

        //first want instance from basket repo ask clr for this

        public BasketsController(IBasketRepository BasketRepo)
        {
            _basketRepo = BasketRepo;
        }

        // getBasket

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string BasketId)
        {
            var Basket = await _basketRepo.GetBasketAsync(BasketId);
            return Ok(Basket is null ? new CustomerBasket(BasketId) : Basket);
        }

        // create or update basket
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket Basket)
        {
            var UpdatedBasket = await _basketRepo.UpdateBasketAsync(Basket);
            if (UpdatedBasket is null) return BadRequest(new ApiResponse(400));
            return Ok(UpdatedBasket);
        }


        // delete basket

        [HttpDelete]
        public async Task<bool> DeleteBasket(string BasketId)
        {
            var Deleted = await _basketRepo.DeleteBasketAsync(BasketId);
            return Deleted;
        }
    }
}
