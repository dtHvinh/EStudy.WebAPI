using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebAPI.Data;

namespace WebAPI.Endpoints.AdminEndpoints.UserStatistic;

public class Endpoint(ApplicationDbContext context) : Endpoint<UserStatisticRequest, List<UserStatisticResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("user-statistics/{MonthRange}/month");
        Description(d => d
            .WithName("Get User Statistics")
            .WithDescription("Retrieve user statistics for the specified month range")
            .Produces<UserStatisticResponse[]>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags("Admin"));
        Group<AdminGroup>();
    }
    public override async Task HandleAsync(UserStatisticRequest req, CancellationToken ct)
    {
        var monthData = await _context.Users
            .GroupBy(u => u.CreationDate.Month)
            .Select(g => new
            {
                MonthNumber = g.Key,
                UserCount = g.Count()
            })
            .OrderBy(x => x.MonthNumber)
            .ToListAsync(ct);

        var stats = monthData
            .Select(x => new UserStatisticResponse
            {
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(x.MonthNumber),
                User = x.UserCount
            })
            .ToList();

        await SendOkAsync(stats, ct);
    }
}
