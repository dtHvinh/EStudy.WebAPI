using StackExchange.Redis;
using StackExchange.Redis.KeyspaceIsolation;
using WebAPI.Services.Contract;
using WebAPI.Utilities.Attributes;

namespace WebAPI.Services;

[Service(ServiceLifetime.Singleton)]
public class RedisService(IConnectionMultiplexer muxer, IConfiguration configuration) : IRedisService
{
    private readonly IDatabase _database = muxer.GetDatabase().WithKeyPrefix(configuration["Redis:KeyPrefix"]);

    public async Task<string?> GetStringAsync(string key)
    {
        return await _database.StringGetAsync(key);
    }

    public async Task SetStringAsync(string key, string value, TimeSpan? expiry = null)
    {
        await _database.StringSetAsync(key, value, expiry);
    }

    public async Task<bool> KeyExistsAsync(string key)
    {
        return await _database.KeyExistsAsync(key);
    }

    public async Task DeleteKeyAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}
