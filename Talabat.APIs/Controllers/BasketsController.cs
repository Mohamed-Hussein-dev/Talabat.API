using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers
{

    public class BasketsController : APIBaseController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper mapper;

        //first want instance from basket repo ask clr for this

        public BasketsController(IBasketRepository BasketRepo , IMapper mapper)
        {
            _basketRepo = BasketRepo;
            this.mapper = mapper;
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
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto Basket)
        {
            var MappedBasket = mapper.Map<CustomerBasket>(Basket);
            var UpdatedBasket = await _basketRepo.UpdateBasketAsync(MappedBasket);

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
