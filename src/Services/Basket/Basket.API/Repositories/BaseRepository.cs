using System;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        private readonly IDistributedCache _redisCache;

        public BaseRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task DeleteBasket(string key)
        {
            await _redisCache.RemoveAsync(key);
        }

        public async Task<T> GetModel(string key)
        {
            var model = await _redisCache.GetStringAsync(key);

            T result = null;

            if (model != null)
            {
                result = JsonConvert.DeserializeObject<T>(model);
            }

            return result;
        }

        public async Task<T> UpdateModel(string key, T model)
        {
            if (model != null)
            {
                await _redisCache.SetStringAsync(key, JsonConvert.SerializeObject(model));
            }

            return await GetModel(key);
        }
    }
}

