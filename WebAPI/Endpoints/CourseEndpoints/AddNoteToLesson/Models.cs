namespace WebAPI.Endpoints.CourseEndpoints.AddNoteToLesson;

public class AddNoteToLessonRequest
{
    public int LessonId { get; set; }
    public string Content { get; set; } = string.Empty;
}
