using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.CourseEndpoints.GetEnrolledCourse;

public sealed class GetEnrolledCourseRequest : PaginationParams
{
    public string? Query { get; set; } = default!;
}

public sealed class GetEnrolledCourseResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public float CompletionPercentage { get; set; } = 0.0f;
}