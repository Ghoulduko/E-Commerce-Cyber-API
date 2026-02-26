using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Application.Cache;

public class CacheService
{
    private readonly IMemoryCache _cache;

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Set(string key, object value, TimeSpan expiration)
    {
        _cache.Set(key, value, expiration);
    }

    public T? Get<T>(string key)
    {
        return _cache.Get<T>(key);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}
