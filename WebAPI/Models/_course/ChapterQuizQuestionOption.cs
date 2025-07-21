using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._course;

[Table("ChapterQuizQuestionOptions")]
public class ChapterQuizQuestionOption
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;
    public bool IsCorrect { get; set; } = false;

    [ForeignKey(nameof(ChapterQuizQuestionId))]
    public int ChapterQuizQuestionId { get; set; }
    public ChapterQuizQuestion ChapterQuizQuestion { get; set; } = default!;
}
