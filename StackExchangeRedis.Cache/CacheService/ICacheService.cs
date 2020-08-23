using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchangeRedis.Cache.CacheService
{
    public interface ICacheService
    {
        T Get<T>(string key, CommandFlags flags = CommandFlags.None);
        void Add(string key, object data, int expiration,When when = When.Always, CommandFlags flags = CommandFlags.None);
        void Remove(string key, CommandFlags flag = CommandFlags.None);
        bool Any(string key, CommandFlags flag = CommandFlags.None);
        void Clear();


    }
}
