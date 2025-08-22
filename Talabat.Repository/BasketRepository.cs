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
        private readonly IDatabase _context;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _context = redis.GetDatabase();
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _context.StringGetAsync(basketId);
            if (basket.IsNullOrEmpty) return null;

            var result = JsonSerializer.Deserialize<CustomerBasket>(basket);
            return basket.IsNullOrEmpty? null : result;
        }
        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        { 
            var JsonBasket = JsonSerializer.Serialize(basket);
            var created = await _context.StringSetAsync(basket.Id, JsonBasket, TimeSpan.FromHours(1));
            return !created ? null: await GetBasketAsync(basket.Id);
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _context.KeyDeleteAsync(basketId);
        }
        public async Task<bool> BasketExistsAsync(string basketId)
        {
            var basket = await _context.StringGetAsync(basketId);
            return !basket.IsNullOrEmpty;
        }
        public async Task<int> GetBasketItemCountAsync(string basketId)
        {
            var basket = await GetBasketAsync(basketId);
            return basket?.Items.Count ?? 0;
        }
        public async Task<IEnumerable<BasketItem>> GetBasketItemsAsync(string basketId)
        {
            var basket = await GetBasketAsync(basketId);
            return basket?.Items ?? Enumerable.Empty<BasketItem>();
        }
        public async Task<bool> ClearBasketAsync(string basketId)
        {
            var basket = _context.StringGetAsync(basketId);
            return basket.IsCompletedSuccessfully ? await _context.KeyDeleteAsync(basketId) : false;
        }
    }
}
