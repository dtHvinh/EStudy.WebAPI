using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Constants;
using WebAPI.Data;
using WebAPI.Models._report;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.ReportEndpoints.SendReportRequest;

public class Endpoint(ApplicationDbContext context) : Endpoint<SendReportRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("");
        AllowAnonymous();
        Description(x => x.WithSummary("Send a report request."));
        Group<ReportGroup>();
    }
    public override async Task HandleAsync(SendReportRequest request, CancellationToken ct)
    {
        var pendingStatus = await _context.ReportStatuses
            .FirstOrDefaultAsync(x => x.Name == ReportStatuses.Pending, ct);

        if (pendingStatus is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _context.Reports.Add(new Report
        {
            Type = request.Type,
            TargetId = request.TargetId,
            ReportReasonId = request.ReasonId,
            Description = request.Description,
            UserId = int.Parse(this.RetrieveUserId()),
            CreationDate = DateTimeOffset.UtcNow,
            ReportStatusId = pendingStatus.Id
        });

        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
