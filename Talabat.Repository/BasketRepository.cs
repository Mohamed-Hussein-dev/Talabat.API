using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        // need instance from IconnectionMaltuplixer to connect with redis
        // ask CLR to inject Iconnection
        private readonly IDatabase RedisDb;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            RedisDb = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await RedisDb.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var Basket = await RedisDb.StringGetAsync(BasketId);

            if (Basket.IsNull)
                return null;

            return JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
            var jsonBasket = JsonSerializer.Serialize(Basket);

            bool isCreatedOrUpdate = await RedisDb.StringSetAsync(Basket.Id , jsonBasket, TimeSpan.FromDays(1));
            if(!isCreatedOrUpdate) return null;

            return await GetBasketAsync(Basket.Id);

        }
    }
}
