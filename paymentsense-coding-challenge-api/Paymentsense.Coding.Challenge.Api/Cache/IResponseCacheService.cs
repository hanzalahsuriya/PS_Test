using System;
using System.Threading;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Cache
{
    public interface IResponseCacheService
    {
        public Task CacheResponseAsync<T>(string cacheKey, T response, TimeSpan timeLive, CancellationToken cancellationToken);

        public Task<T> GetCachedResponse<T>(string cacheKey, CancellationToken cancellationToken);
    }
}
