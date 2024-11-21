using StackExchange.Redis;
using Store.Repository.Basket.Models;
using System.Text.Json;


namespace Store.Repository.Basket
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketid)
        => await _database.KeyDeleteAsync(basketid);

        public async Task<CustomerBasket> GetBasketAsync(string basketid)
        {
            var Basket = await _database.StringGetAsync(basketid);
            return Basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);

        }
       
        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var IsCreated = await _database.StringSetAsync(customerBasket.Id, 
                                               JsonSerializer.Serialize(customerBasket), TimeSpan.FromDays(3));
            if (!IsCreated)
            {
                return null;
            }
            return await GetBasketAsync(customerBasket.Id);

        }
    }
}
