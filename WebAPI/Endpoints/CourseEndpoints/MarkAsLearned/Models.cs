namespace WebAPI.Endpoints.CourseEndpoints.MarkAsLearned;

public sealed class MarkAsLearnRequest
{
    public int CourseId { get; set; }
    public int LessonId { get; set; }
}

public sealed class MarkAsLearnResponse
{
}