using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;

namespace WebAPI.Models._course;

[Table("LessonProgress")]
public class LessonProgress
{
    public int Id { get; set; }

    [ForeignKey(nameof(Lesson))]
    public int LessonId { get; set; }
    public CourseLesson Lesson { get; set; } = default!;

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public bool IsCompleted { get; set; } = false;
    public DateTimeOffset? CompletionDate { get; set; }
    public DateTimeOffset? LastWatchedDate { get; set; }
    public int WatchTimeSeconds { get; set; } = 0;
    public int WatchPercentage { get; set; } = 0;
}