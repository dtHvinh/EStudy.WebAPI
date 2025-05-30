using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

[Table("Courses")]
public class Course
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }

    [ForeignKey(nameof(Instructor))]
    public int InstructorId { get; set; }
    public User Instructor { get; set; } = default!;

    public ICollection<Chapter> Chapters { get; set; } = default!;
    public ICollection<CourseEnrollment> Enrollments { get; set; } = default!;
}
