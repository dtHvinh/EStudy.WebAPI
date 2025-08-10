using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._testExam;

[Table("TestAnswerSelections")]
public class TestAnswerSelection
{
    public int Id { get; set; }

    [ForeignKey(nameof(TestAttempt))]
    public int TestAttemptId { get; set; }
    public TestAttempt TestAttempt { get; set; } = default!;

    [ForeignKey(nameof(Question))]
    public int QuestionId { get; set; }
    public Question Question { get; set; } = default!;

    [ForeignKey(nameof(SelectedAnswer))]
    public int SelectedAnswerId { get; set; }
    public Answer SelectedAnswer { get; set; } = default!;
}
