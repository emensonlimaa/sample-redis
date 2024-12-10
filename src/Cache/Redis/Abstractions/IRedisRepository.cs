namespace Cache.Redis.Abstractions;

public interface IRedisRepository
{
    Task Insert<T>(string key, T data, long timeToLive);
    Task Delete(string key);
    Task<T> Read<T>(string key);
}
