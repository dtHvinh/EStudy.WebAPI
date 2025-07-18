using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;

namespace WebAPI.Models._course;

[Table("LessonProgress")]
[Index(nameof(LessonId), nameof(UserId), IsUnique = true)]
public class LessonProgress
{
    public int Id { get; set; }

    [ForeignKey(nameof(Lesson))]
    public int LessonId { get; set; }
    public CourseLesson Lesson { get; set; } = default!;

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public DateTimeOffset? CompletionDate { get; set; }
}