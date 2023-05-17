using StackExchange.Redis;

namespace RedisExchangeAPI.WebApp.Services
{
    public class RedisService
    {
        private readonly string redisHost;
        private readonly string redisPort;

        private ConnectionMultiplexer redis;

        public IDatabase db { get; set; }

        public RedisService(IConfiguration configuration)
        {
            redisHost = configuration["Redis:Host"];
            redisPort = configuration["Redis:Port"];
        }

        public void Connect()
        {
            var configString = $"{redisHost}:{redisPort}";
            redis = ConnectionMultiplexer.Connect(configString);
        }

        public IDatabase GetDatabase(int db)
        {
            return redis.GetDatabase(db);
        }
    }
}