using FastEndpoints;
using Stripe;
using System.Globalization;
using WebAPI.Data;

namespace WebAPI.Endpoints.AdminEndpoints.CourseRevenue;

public class Endpoint(ApplicationDbContext context) : Endpoint<CourseRevenueRequest, List<CourseRevenueResponse>>
{
    private readonly ApplicationDbContext _context = context;
    public override void Configure()
    {
        Get("course-revenue/{MonthRange}/month");
        Description(d => d
            .WithName("Get Course Revenue")
            .WithDescription("Retrieve course revenue for the specified month range")
            .Produces<CourseRevenueResponse[]>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags("Admin"));
        Group<AdminGroup>();
    }
    public override async Task HandleAsync(CourseRevenueRequest req, CancellationToken ct)
    {
        var service = new PaymentIntentService();

        var options = new PaymentIntentListOptions
        {
            Limit = 100,
            Created = new DateRangeOptions
            {
                GreaterThanOrEqual = DateTime.UtcNow.AddMonths(-1 * req.MonthRange)
            }
        };

        var paymentIntents = await service.ListAsync(options, cancellationToken: ct);

        var stats = paymentIntents
            .Where(pi => pi.Status == "succeeded")
            .GroupBy(pi => new { pi.Created.Year, pi.Created.Month })
            .Select(g => new CourseRevenueResponse
            {
                Month = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month)} {g.Key.Year}",
                Revenue = g.Sum(x => x.AmountReceived) / 100m
            })
            .OrderBy(x => DateTime.ParseExact(x.Month, "MMMM yyyy", CultureInfo.CurrentCulture))
            .ToList();

        await SendOkAsync(stats, ct);
    }
}
