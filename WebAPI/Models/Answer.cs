using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

[Table("Answers")]
public class Answer
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public bool IsCorrect { get; set; }

    [ForeignKey(nameof(Question))]
    public int QuestionId { get; set; }
    public Question Question { get; set; } = default!;
}
