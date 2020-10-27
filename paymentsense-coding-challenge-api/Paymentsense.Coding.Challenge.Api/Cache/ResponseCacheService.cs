using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Paymentsense.Coding.Challenge.Api.Cache
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public ResponseCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task CacheResponseAsync<T>(string cacheKey, T response, TimeSpan timeLive, CancellationToken cancellationToken)
        {
            if (response == null)
            {
                return;
            }

            var serializedResponse = JsonSerializer.Serialize(response);
            await _distributedCache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = timeLive
            }, cancellationToken);

        }

        public async Task<T> GetCachedResponse<T>(string cacheKey, CancellationToken cancellationToken)
        {
            var cachedResponse = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
            return string.IsNullOrWhiteSpace(cachedResponse) ? default : JsonSerializer.Deserialize<T>(cachedResponse);
        }
    }
}
