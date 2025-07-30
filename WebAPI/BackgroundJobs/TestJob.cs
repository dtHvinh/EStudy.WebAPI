using Quartz;

namespace WebAPI.BackgroundJobs;

public class TestJob(Serilog.ILogger logger) : IJob
{
    private readonly Serilog.ILogger _logger = logger;

    public static readonly DateTimeOffset StartAt = DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(3));

    public static CalendarIntervalScheduleBuilder GetJobScheduler()
    {
        return CalendarIntervalScheduleBuilder
                                .Create()
                                .WithIntervalInSeconds(35)
                                .WithMisfireHandlingInstructionFireAndProceed();
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.Information("Job executed at {Time}", DateTimeOffset.UtcNow);

        return Task.CompletedTask;
    }
}
