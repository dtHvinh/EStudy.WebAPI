using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.TestEndpoints.GetTest;

public sealed class GetTestRequest : PaginationParams
{

}

public sealed class GetTestResponse
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }

    public int SectionCount { get; set; }
    public int AttemptCount { get; set; }
    public int CommentCount { get; set; }
    public int QuestionCount { get; set; }
}
