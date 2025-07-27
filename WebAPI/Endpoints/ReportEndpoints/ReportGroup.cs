using FastEndpoints;

namespace WebAPI.Endpoints.ReportEndpoints;

public class ReportGroup : Group
{
    public ReportGroup()
    {
        Configure("reports", ep =>
        {
            ep.Description(d => d.WithTags("Report"));
        });
    }
}
