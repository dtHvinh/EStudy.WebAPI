using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.AppEndpoints;

public sealed class GetReportReasonsResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class GetReportReasonsEndpoint(ApplicationDbContext context) : EndpointWithoutRequest<List<GetReportReasonsResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("report-reasons");
        AllowAnonymous();
        Throttle(20, 60);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var roles = await _context.ReportReasons
            .Select(role => new GetReportReasonsResponse
            {
                Id = role.Id,
                Title = role.Title!,
                Description = role.Description!
            })
            .ToListAsync(ct);

        await SendOkAsync(roles, ct);
    }
}
