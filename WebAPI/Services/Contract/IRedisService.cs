
namespace WebAPI.Services.Contract;

public interface IRedisService
{
    Task DeleteKeyAsync(string key);
    Task<string?> GetStringAsync(string key);
    Task<bool> KeyExistsAsync(string key);
    Task SetStringAsync(string key, string value, TimeSpan? expiry = null);
}
