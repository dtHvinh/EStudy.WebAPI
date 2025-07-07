using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.CourseEndpoints.GetMyCourses;

public sealed class GetMyCourseRequest : PaginationParams;

public sealed class GetMyCourseResponse
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
    public string? Prerequisites { get; set; } // Line separated string of prerequisites
    public string? LearningObjectives { get; set; } // Line separated string of learning objectives
    public string? Language { get; set; } = default!;
    public int EstimatedDurationHours { get; set; } = 0;
    public int StudentsCount { get; set; } = 0;
}
