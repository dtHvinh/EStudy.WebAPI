using Quartz;
using WebAPI.Constants;
using WebAPI.Data;
using WebAPI.Utilities.Extensions.LoggerExtensions;

namespace WebAPI.BackgroundJobs;

public class PaymentIntentCleanUpJob(Serilog.ILogger logger, ApplicationDbContext context) : IJob
{
    private readonly Serilog.ILogger _logger = logger;
    private readonly ApplicationDbContext _context = context;
    private const int PaymentIntentDateThreshold = 7;

    public static readonly DateTimeOffset StartAt = DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(3));

    public static CalendarIntervalScheduleBuilder GetJobScheduler()
    {
        return CalendarIntervalScheduleBuilder
                                .Create()
                                .WithIntervalInMinutes(5)
                                .WithMisfireHandlingInstructionFireAndProceed();
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            // Find all payment intents that are not succeeded and older than the threshold
            var paymentIntents = _context.Transactions
                .Where(e => e.Status != PaymentIntentStatus.Succeeded
                         && e.CreationDate.AddDays(PaymentIntentDateThreshold) < DateTimeOffset.UtcNow);

            _context.Transactions.RemoveRange(paymentIntents);

            int affectedRows = await _context.SaveChangesAsync();

            _logger.PaymentIntentCleanedUp(affectedRows);
        }
        catch (Exception ex)
        {
            _logger.Debug(ex.Message);
        }
    }
}
