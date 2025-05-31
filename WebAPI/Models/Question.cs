using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

public class Question
{
    public int Id { get; set; }
    public required string Text { get; set; }

    [ForeignKey(nameof(TestExam))]
    public int ExamId { get; set; }
    public TestExam Exam { get; set; } = default!;

    public ICollection<Answer> Answers { get; set; } = default!;
}