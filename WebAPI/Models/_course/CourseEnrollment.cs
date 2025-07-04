using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;

namespace WebAPI.Models._course;

[Table("CourseEnrollments")]
public class CourseEnrollment
{
    public int Id { get; set; }

    [ForeignKey(nameof(Course))]
    public int CourseId { get; set; }
    public Course Course { get; set; } = default!;

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public DateTimeOffset EnrollmentDate { get; set; }
    public DateTimeOffset? CompletionDate { get; set; }
    public int ProgressPercentage { get; set; } = 0;
    public DateTimeOffset? LastAccessDate { get; set; }
    public bool IsCompleted { get; set; } = false;
    public bool IsFavorite { get; set; } = false;
    public int TimeSpentMinutes { get; set; } = 0;
}