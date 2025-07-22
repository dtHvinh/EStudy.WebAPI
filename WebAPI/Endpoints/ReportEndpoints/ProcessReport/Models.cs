namespace WebAPI.Endpoints.ReportEndpoints.ProcessReport;

public sealed class ProcessReportRequest
{
    public int ReportId { get; set; }
    public string Action { get; set; } = default!;
}
