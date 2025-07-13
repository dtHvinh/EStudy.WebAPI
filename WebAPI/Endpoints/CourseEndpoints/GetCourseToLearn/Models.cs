namespace WebAPI.Endpoints.CourseEndpoints.GetCourseToLearn;

public sealed class GetCourseToLearnRequest
{
    public int CourseId { get; set; }
}

public sealed class GetCourseToLearnResponse
{
    public int StudentCount { get; set; }
    public string Title { get; set; } = default!;
    public double AverageRating { get; set; }
    public List<GetCourseToLearnChapterResponse> Chapters { get; set; } = default!;
}

public class GetCourseToLearnChapterResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public int OrderIndex { get; set; }
    public bool IsPublished { get; set; }
    public int TotalMinutes { get; set; }
    public List<GetCourseToLearnLessonResponse> Lessons { get; set; } = default!;
}

public class GetCourseToLearnLessonResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<string> AttachedFileUrls { get; set; } = default!;
    public string Content { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DurationMinutes { get; set; }
    public int OrderIndex { get; set; }
    public string? TranscriptUrl { get; set; }
    public GetCourseToLearnNote? Note { get; set; } = default!;
    public string? VideoUrl { get; set; }
    public bool IsCompleted { get; set; } = false;
}

public class GetCourseToLearnNote
{
    public string Content { get; set; } = default!;
}