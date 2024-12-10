using StackExchange.Redis;

namespace Cache.Redis.Abstractions;

public interface IRedisClient
{
    Task<IDatabaseAsync> GetConnection();
}
