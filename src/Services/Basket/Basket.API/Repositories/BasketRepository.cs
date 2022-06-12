using System;
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Repositories
{
    public class BasketRepository : BaseRepository<ShoppingCart>, IBasketRepository
    {
        public BasketRepository(IDistributedCache redisCache) : base(redisCache)
        {
        }
    }
}

