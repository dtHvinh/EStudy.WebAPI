using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._course;

[Table("ChapterQuizzes")]
public class ChapterQuiz
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public int OrderIndex { get; set; }

    [ForeignKey(nameof(Chapter))]
    public int ChapterId { get; set; }
    public CourseChapter Chapter { get; set; } = default!;

    public ICollection<ChapterQuizQuestion> Questions { get; set; } = default!;
    public ICollection<QuizProgress> QuizProgress { get; set; } = default!;
}
