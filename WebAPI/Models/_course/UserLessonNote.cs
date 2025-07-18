using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;
using WebAPI.Models.Contract;

namespace WebAPI.Models._course;

[Table("UserLessonNotes")]
public class UserLessonNote : IEntityWithTime<int>
{
    public int Id { get; set; }
    public string Content { get; set; } = default!;
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset? ModificationDate { get; set; }

    public int LessonId { get; set; }
    public CourseLesson Lesson { get; set; } = default!;

    public int UserId { get; set; }
    public User User { get; set; } = default!;
}
