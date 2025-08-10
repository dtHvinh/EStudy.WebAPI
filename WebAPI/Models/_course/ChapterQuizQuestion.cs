using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._course;

[Table("ChapterQuizQuestions")]
public class ChapterQuizQuestion
{
    public int Id { get; set; } = 0;
    public string QuestionText { get; set; } = default!;

    [ForeignKey(nameof(ChapterId))]
    public int ChapterId { get; set; }
    public ChapterQuiz Chapter { get; set; } = default!;

    public ICollection<ChapterQuizQuestionOption> Options { get; set; } = default!;
}
