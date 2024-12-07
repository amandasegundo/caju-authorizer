using caju_authorizer_domain.Authorizer.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace caju_authorizer_infra.data.Repositories.Cache
{
  public class CacheRepository(IMemoryCache cache): ICacheRepository
  {
    public string Get(string key)
    {
      cache.TryGetValue(key, out string item);
      return item;
    }
    public void Set(string key, string value, int expirationInMinutes)
    {
      var cacheEntryOptions = new MemoryCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromMinutes(expirationInMinutes));
      cache.Set(key, value, cacheEntryOptions);
    }
  }
}
