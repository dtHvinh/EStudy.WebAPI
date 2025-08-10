namespace WebAPI.Utilities.Extensions.LoggerExtensions;

public static class TransactionJobLoggerExtensions
{
    public static void PaymentIntentCleanedUp(this Serilog.ILogger logger, int affectedRows)
    {
        logger.Information("{AffectedRows} payment intents have been cleaned up", affectedRows);
    }
}
