namespace WebAPI.Endpoints.CourseEndpoints.EditCourseStructure;

public class CourseChapterRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public int OrderIndex { get; set; }
    public bool IsPublished { get; set; }
    public List<CourseLessonRequest> Lessons { get; set; } = default!;
}

public class CourseLessonRequest
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

public class EditCourseStructureRequest
{
    public int CourseId { get; set; }
    public bool IsPublished { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public string DifficultyLevel { get; set; } = "Beginner";
    public decimal Price { get; set; } = 0;
    public bool IsFree { get; set; } = true;
    public string? Prerequisites { get; set; }
    public string? LearningObjectives { get; set; }
    public string? Language { get; set; } = "English";
    public int EstimatedDurationHours { get; set; } = 0;

    public List<CourseChapterRequest> Chapters { get; set; } = default!;
}

public class EditCourseStructureResponse
{
    public int CourseId { get; set; }
}