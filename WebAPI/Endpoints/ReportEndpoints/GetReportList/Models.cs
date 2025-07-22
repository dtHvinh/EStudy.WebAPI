using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.ReportEndpoints.GetReportList;

public sealed class GetReportListRequest : PaginationParams
{
    public string Status { get; set; } = default!;
}

public sealed class GetReportListResponse
{
    public int Id { get; init; }
    public string ReporterName { get; init; } = default!;
    public string? ReporterEmail { get; init; } = default!;

    public string TargetType { get; init; } = default!;
    public int TargetId { get; init; } = default!;
    public string TargetTitle { get; set; } = default!;

    public string Reason { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string Status { get; init; } = default!;
    public DateTimeOffset CreationDate { get; init; }
}