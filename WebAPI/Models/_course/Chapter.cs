using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._course;

[Table("Chapters")]
public class Chapter
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }

    [ForeignKey("Course")]
    public int CourseId { get; set; }
    public Course Course { get; set; } = default!;

    public ICollection<Lesson> Lessons { get; set; } = default!;
}
