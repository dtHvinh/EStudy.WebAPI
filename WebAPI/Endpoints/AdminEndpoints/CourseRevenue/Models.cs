namespace WebAPI.Endpoints.AdminEndpoints.CourseRevenue;

public sealed class CourseRevenueRequest
{
    public int MonthRange { get; set; }
}

public sealed class CourseRevenueResponse
{
    public string Month { get; set; }
    public decimal Revenue { get; set; }
}