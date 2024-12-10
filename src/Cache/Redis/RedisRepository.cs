using Cache.Redis.Abstractions;
using StackExchange.Redis;
using System.Text.Json;

namespace Cache.Redis;

public class RedisRepository : IRedisRepository
{
    private readonly IRedisClient _client;
    private static IDatabaseAsync database;

    public RedisRepository(IRedisClient client)
    {
        _client = client;
    }

    public async Task Delete(string key)
    {
        try
        {
            database = await _client.GetConnection();

            await database.KeyDeleteAsync(key);
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    public async Task Insert<T>(string key, T data, long timeToLive)
    {
        try
        {
            database = await _client.GetConnection();

            await database.StringSetAsync(key, JsonSerializer.Serialize(data), TimeSpan.FromMilliseconds(timeToLive));
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<T> Read<T>(string key)
    {
        try
        {
            database = await _client.GetConnection();

            var result = await database.StringGetAsync(key);

            if (result.HasValue)
            {
                return (T)Convert.ChangeType(result.ToString(), typeof(T));
            }
            else
            {
                return default;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
