using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.TestEndpoints.GetListAttemptPreview;

public sealed class GetListAttemptPreviewRequest : PaginationParams
{
    public int TestId { get; set; }
}

public sealed class GetListAttemptPreviewResponse
{
    public int Id { get; set; }
    public int EarnedPoints { get; set; }
    public DateTimeOffset SubmitDate { get; set; }
    public int TimeSpent { get; set; }
}