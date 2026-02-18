using Domain.Contracts;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    internal class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string CacheKey)
        {
            return await _cacheRepository.GetAsync(CacheKey);
        }

        public async Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive)
        {
            var value = JsonSerializer.Serialize(CacheValue);
            await _cacheRepository.SetAsync(CacheKey, value, TimeToLive);
        }
    }
}
