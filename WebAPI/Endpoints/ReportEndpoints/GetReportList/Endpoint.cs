using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;
using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.ReportEndpoints.GetReportList;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetReportListRequest, PagedResponse<GetReportListResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        AllowAnonymous();
        Description(x => x.WithSummary("Get reports."));
        Group<ReportGroup>();
    }
    public override async Task HandleAsync(GetReportListRequest request, CancellationToken ct)
    {
        var reports = await _context.Reports
            .FilterReport(request.Status)
            .Select(e => new GetReportListResponse
            {
                Id = e.Id,
                ReporterName = e.User!.Name,
                ReporterEmail = e.User.Email,

                TargetType = e.Type,
                TargetId = e.TargetId,

                Description = e.Description,
                Reason = e.ReportReason!.Title,
                Status = e.ReportStatus.Name,
                CreationDate = e.CreationDate,
            })
            .Paginate(request.Page, request.PageSize)
            .ToListAsync(ct);

        foreach (var report in reports)
        {
            report.TargetTitle = await GetTargetTitle(report.TargetType, report.TargetId, ct);
        }

        var totalCount = await _context.Reports.CountAsync(ct);

        var response = new PagedResponse<GetReportListResponse>
        {
            Items = reports,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize)
        };

        await SendOkAsync(response, ct);
    }

    private async Task<string> GetTargetTitle(string type, int targetId, CancellationToken ct = default)
    {
        return type switch
        {
            "user" => await _context.Users.Where(u => u.Id == targetId).Select(u => u.Name).FirstOrDefaultAsync(ct) ?? "Unknown User",
            "blog" => await _context.Blogs.Where(p => p.Id == targetId).Select(p => p.Title).FirstOrDefaultAsync(ct) ?? "Unknown Post",
            _ => "Unknown Target"
        };
    }
}
