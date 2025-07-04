using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.Contract;

namespace WebAPI.Models._course;

[Table("CourseLessons")]
public class CourseLesson : IOrderedItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string VideoUrl { get; set; }
    public string Content { get; set; } = default!;
    public string? Description { get; set; }
    public int DurationMinutes { get; set; } = 0;
    public int OrderIndex { get; set; } = 0;
    public string? TranscriptUrl { get; set; }
    public string? ThumbnailUrl { get; set; }


    [ForeignKey(nameof(Chapter))]
    public int ChapterId { get; set; }
    public CourseChapter Chapter { get; set; } = default!;
    public ICollection<LessonProgress> LessonProgress { get; set; } = default!;
}