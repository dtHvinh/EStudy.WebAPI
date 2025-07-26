using WebAPI.Services.Contract;
using WebAPI.Utilities.Attributes;

namespace WebAPI.Services;

[Service(ServiceLifetime.Singleton)]
public class BanService(IRedisService redisService) : IBanService
{
    private const string BanKeyPrefix = "ban:";

    private readonly IRedisService _redisService = redisService;

    private static string Key(string userId) => $"{BanKeyPrefix}{userId}";

    public async Task<bool> IsUserBannedAsync(string userId)
    {
        var banKey = Key(userId);
        var isBanned = await _redisService.KeyExistsAsync(banKey);
        return isBanned;
    }

    public async Task BanUserAsync(string userId, TimeSpan? expiry = null)
    {
        var banKey = Key(userId);
        var expriryDueDate = expiry.HasValue ? DateTimeOffset.UtcNow.Add(expiry.Value).ToString() : "Permanent";
        await _redisService.SetStringAsync(banKey, expriryDueDate, expiry);
    }

    public async Task UnbanUserAsync(string userId)
    {
        var banKey = Key(userId);
        await _redisService.DeleteKeyAsync(banKey);
    }

    public async Task<DateTimeOffset?> GetBanDueDateAsync(string userId)
    {
        var banKey = Key(userId);
        var banDueDate = await _redisService.GetStringAsync(banKey);
        return banDueDate is not null
            ? DateTimeOffset.Parse(banDueDate) : null;
    }
}
