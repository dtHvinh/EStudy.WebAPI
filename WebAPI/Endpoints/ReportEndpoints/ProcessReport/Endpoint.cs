using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.ReportEndpoints.ProcessReport;

public class Endpoint(ApplicationDbContext context) : Endpoint<ProcessReportRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("{ReportId}");
        Description(x => x.WithSummary("Process reports."));
        Group<ReportGroup>();
    }

    public override async Task HandleAsync(ProcessReportRequest req, CancellationToken ct)
    {
        var report = await _context.Reports.FindAsync([req.ReportId], ct);
        if (report == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (req.Action == "warn-user")
        {
            return; // TODO: Implement warn user logic
        }

        // Action can be "Resolved" or "Rejected" or "UnderReview"
        var status = await _context.ReportStatuses.FirstOrDefaultAsync(e => e.Name == req.Action, ct);
        if (status == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        report.ReportStatus = status;
        _context.Reports.Update(report);

        await _context.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }
}
