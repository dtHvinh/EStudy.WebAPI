

namespace WebAPI.Services.Contract;

public interface IBanService
{
    Task BanUserAsync(string userId, TimeSpan? expiry = null);
    Task<DateTimeOffset?> GetBanDueDateAsync(string userId);
    Task<bool> IsUserBannedAsync(string userId);
    Task UnbanUserAsync(string userId);
}
