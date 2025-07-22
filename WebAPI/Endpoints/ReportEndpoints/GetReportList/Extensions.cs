using WebAPI.Models._report;

namespace WebAPI.Endpoints.ReportEndpoints.GetReportList;

public static class Extensions
{
    public static IQueryable<Report> FilterReport(this IQueryable<Report> query, string? status)
    {
        if (string.IsNullOrWhiteSpace(status) || string.Equals(status, "all", StringComparison.InvariantCultureIgnoreCase))
        {
            return query;
        }

        return query.Where(e => e.ReportStatus.Name.Equals(status));
    }
}
