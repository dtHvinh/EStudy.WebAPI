namespace WebAPI.Endpoints.ReportEndpoints.SendReportRequest;

public sealed class SendReportRequest
{
    public string Type { get; set; } = default!;
    public int TargetId { get; set; }
    public int ReasonId { get; set; } = default!;
    public string Description { get; set; } = default!;
}
