using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.CourseEndpoints.GetCourses;

public sealed class GetCoursesRequest : PaginationParams
{
}

public sealed class GetCourseResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset ModificationDate { get; set; }
    public string? ImageUrl { get; set; }
    public string DifficultyLevel { get; set; } = default!;
    public decimal Price { get; set; } = 0;
    public bool IsFree { get; set; } = true;
    public bool IsPublished { get; set; } = false;
    public string? Prerequisites { get; set; }
    public string? LearningObjectives { get; set; }
    public string? Language { get; set; } = default!;
    public int EstimatedDurationHours { get; set; } = 0;
    public int StudentsCount { get; set; } = 0;
    public bool IsEnrolled { get; set; } = false;
}