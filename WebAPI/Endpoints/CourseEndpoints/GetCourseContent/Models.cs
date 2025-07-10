namespace WebAPI.Endpoints.CourseEndpoints.GetCourseContent;

public sealed class GetCouseContentRequest
{
    public int CourseId { get; set; }
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
    public List<GetCourseContentChapterResponse> Chapters { get; set; } = default!;
}

public class GetCourseContentChapterResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public int OrderIndex { get; set; }
    public bool IsPublished { get; set; }
    public List<GetCourseContentLessonResponse> Lessons { get; set; } = default!;
}

public class GetCourseContentLessonResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<string> AttachedFileUrls { get; set; } = default!;
    public string Content { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DurationMinutes { get; set; }
    public int OrderIndex { get; set; }
    public string? TranscriptUrl { get; set; }
    public string? VideoUrl { get; set; }
}