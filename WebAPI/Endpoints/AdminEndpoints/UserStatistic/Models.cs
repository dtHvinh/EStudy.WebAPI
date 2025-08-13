namespace WebAPI.Endpoints.AdminEndpoints.UserStatistic;

public sealed class UserStatisticRequest
{
    public int MonthRange { get; set; }
}

public sealed class UserStatisticResponse
{
    public string Month { get; set; } = string.Empty;
    public int User { get; set; }
}
