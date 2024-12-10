using Cache.Redis.Abstractions;
using StackExchange.Redis;

namespace Cache.Redis;

public class RedisClient : IRedisClient
{
    private static IConnectionMultiplexer connectionMultiplexer;
    private static IDatabaseAsync database;
    private const string RedisConnectionString = "localhost:6379";

    public async Task<IDatabaseAsync> GetConnection()
    {
        if (connectionMultiplexer == null || connectionMultiplexer.IsConnected)
        {
            connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(RedisConnectionString);

        }

        database = connectionMultiplexer.GetDatabase();

        return database;
    }
}
