using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.Contract;

namespace WebAPI.Models._course;

[Table("CourseChapters")]
public class CourseChapter : IOrderedItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int OrderIndex { get; set; } = 0;
    public bool IsPublished { get; set; } = true;

    [ForeignKey("Course")]
    public int CourseId { get; set; }
    public Course Course { get; set; } = default!;

    public ICollection<CourseLesson> Lessons { get; set; } = default!;
}