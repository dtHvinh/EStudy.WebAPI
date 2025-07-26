namespace WebAPI.Utilities.Extensions.LoggerExtensions;

public static class UserBanLoggerExtensions
{
    public static void UserBanned(this Serilog.ILogger logger, string userId, string reason)
    {
        logger.Information("User {UserId} has been banned for the following reason: {Reason}", userId, reason);
    }
}
