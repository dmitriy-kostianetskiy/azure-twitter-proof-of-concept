using AzureTwitter.CacheService.Interfaces;
using AzureTwitter.CommunicationUtils;
using StackExchange.Redis;

namespace AzureTwitter.RedisCache
{
    public class RedisCache : ICacheService
    {
        private readonly IDatabase _database;
        private readonly IDataSerializer _dataSerializer;

        public RedisCache(string redisServerAddress, IDataSerializer dataSerializer)
        {
            _dataSerializer = dataSerializer;
            var connection =
                ConnectionMultiplexer.Connect(new ConfigurationOptions()
                {
                    EndPoints = { redisServerAddress }
                });
            _database = connection.GetDatabase();
        }

        public void Set<T>(string key, T data)
        {
            var cacheData = _dataSerializer.Serialize(data);
            _database.StringSet(key, cacheData);
        }

        public T Get<T>(string key)
        {
            var cacheData = _database.StringGet(key);
            return _dataSerializer.Deserialize<T>(cacheData);
        }

        public T GetSet<T>(string key, T data)
        {
            var cacheData = _dataSerializer.Serialize(data);
            var oldCacheData = _database.StringGetSet(key, cacheData);
            return _dataSerializer.Deserialize<T>(oldCacheData);
        }
    }
}
