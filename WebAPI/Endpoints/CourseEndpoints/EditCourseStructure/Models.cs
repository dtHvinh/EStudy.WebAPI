namespace WebAPI.Endpoints.CourseEndpoints.EditCourseStructure;

public sealed class CourseChapterRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public int OrderIndex { get; set; }
    public bool IsPublished { get; set; }
    public List<CourseLessonRequest> Lessons { get; set; } = default!;
    public List<CourseQuizRequest> Quizzes { get; set; } = default!;
}

public sealed class CourseQuizRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public int OrderIndex { get; set; }
    public List<CourseQuizQuestionRequest> Questions { get; set; } = default!;
}

public sealed class CourseQuizQuestionRequest
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;
    public List<CourseQuizQuestionOptionRequest> Options { get; set; } = default!;
}

public sealed class CourseQuizQuestionOptionRequest
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;
    public bool IsCorrect { get; set; } = false;
}

public sealed class CourseLessonRequest
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

public sealed class EditCourseStructureRequest
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

public sealed class EditCourseStructureResponse
{
    public int CourseId { get; set; }
}