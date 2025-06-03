using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._course;

[Table("Lessons")]
public class Lesson
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string VideoUrl { get; set; }

    [ForeignKey(nameof(Chapter))]
    public int ChapterId { get; set; }
    public Chapter Chapter { get; set; } = default!;
}
