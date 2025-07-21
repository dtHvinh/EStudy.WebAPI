using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;

namespace WebAPI.Models._course;

[Table("QuizProgress")]
[Index(nameof(QuizId), nameof(UserId), IsUnique = true)]
public class QuizProgress
{
    public int Id { get; set; }

    [ForeignKey(nameof(Quiz))]
    public int QuizId { get; set; }
    public CourseLesson Quiz { get; set; } = default!;

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public DateTimeOffset? CompletionDate { get; set; }
}