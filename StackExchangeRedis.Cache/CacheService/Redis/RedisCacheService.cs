using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchangeRedis.Cache.CacheService.Redis
{
    public class RedisCacheService : ICacheService
    {
        private RedisServer _redisServer;

        public RedisCacheService(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public void Add(string key, object data, int expiration, When when = When.Always, CommandFlags flag = CommandFlags.None)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            _redisServer.Database.StringSet(key, jsonData, TimeSpan.FromMinutes(expiration), when, flag);
        }

        public bool Any(string key, CommandFlags flag = CommandFlags.None)
        {
            return _redisServer.Database.KeyExists(key, flag);
        }

        public T Get<T>(string key, CommandFlags flag = CommandFlags.None)
        {
            if (Any(key))
            {
                string jsonData = _redisServer.Database.StringGet(key, flag);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            return default;
        }

        public void Remove(string key, CommandFlags flag = CommandFlags.None)
        {
            _redisServer.Database.KeyDelete(key, flag);
        }

        public void Clear()
        {
            _redisServer.FlushDatabase();
        }
    }
}
