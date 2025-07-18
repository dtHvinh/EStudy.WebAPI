using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.CourseEndpoints.GetCourseRatings;

public sealed class GetCourseRatingsRequest : PaginationParams
{
    public int CourseId { get; set; } = default!;
}

public sealed class GetCourseRatingsResponse
{
    public double AverageRating { get; set; }
    public int Id { get; set; }
    public int Value { get; set; }
    public string? Review { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public string UserName { get; set; } = default!;
    public string? UserProfilePicture { get; set; }
}
