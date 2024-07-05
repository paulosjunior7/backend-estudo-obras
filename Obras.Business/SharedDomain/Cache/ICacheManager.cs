using System;
using StackExchange.Redis;
namespace Obras.Business.SharedDomain.Cache
{
    public enum CacheTimeEnum
    {
        Minute = 60,
        TenMinute = 600,
        ThirtyMinute = 1800,
        Hour = 3600,
        FiveHour = 5 * 3600,
        Week = 604800,
        Day = 3600 * 24
    }

    public interface ICacheManager
    {
        T Get<T>(string key);
        string GetString(string key);
        void SetString(string key, string value, CacheTimeEnum cacheTimeEnum = CacheTimeEnum.Hour);
        void SetString(string key, string value, double seconds);
        void Set<T>(string key, T value, CacheTimeEnum cacheTimeEnum = CacheTimeEnum.Hour);
        void Clear(string key);
        void Clear(RedisKey[] keys);
        void ClearAll();

    }
}



