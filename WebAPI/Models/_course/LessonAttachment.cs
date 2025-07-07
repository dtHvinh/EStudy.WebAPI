using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._course;

[Table("CourseAttachments")]
public class LessonAttachment
{
    public int Id { get; set; }
    public required string Url { get; set; }

    [ForeignKey(nameof(Lesson))]
    public int LessonId { get; set; }
    public CourseLesson Lesson { get; set; } = default!;
}
