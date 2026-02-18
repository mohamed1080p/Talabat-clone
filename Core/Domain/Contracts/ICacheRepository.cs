using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICacheRepository
    {
        // get 
        Task<string?> GetAsync(string CacheKey);


        // set
        Task SetAsync(string CacheKey, string CacheValue, TimeSpan TimeToLive);
    }
}
