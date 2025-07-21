namespace WebAPI.Endpoints.CourseEndpoints.GetCourseStructure;

public sealed class GetCourseStructureRequest
{
    public int CourseId { get; set; }
}

public sealed class GetCourseStructureResponse
{
    public int CourseId { get; set; }
    public bool IsPublished { get; set; }
    public List<GetCourseStructureChapterResponse> Chapters { get; set; } = default!;
}

public sealed class GetCourseStructureChapterResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public int OrderIndex { get; set; }
    public bool IsPublished { get; set; }
    public List<GetCourseStructureLessonResponse> Lessons { get; set; } = default!;
    public List<GetCourseStructureQuizResponse> Quizzes { get; set; } = default!;
}

public sealed class GetCourseStructureLessonResponse
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

public sealed class GetCourseStructureQuizResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public int OrderIndex { get; set; }
    public List<GetCourseStructureQuizQuestionResponse> Questions { get; set; } = default!;
}

public sealed class GetCourseStructureQuizQuestionResponse
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;
    public List<GetCourseStructureQuizQuestionOptionResponse> Options { get; set; } = default!;
}

public sealed class GetCourseStructureQuizQuestionOptionResponse
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;
    public bool IsCorrect { get; set; } = false;
}