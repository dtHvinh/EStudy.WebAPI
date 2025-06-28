using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._testExam;

public class Question
{
    public int Id { get; set; }
    public required string Type { get; set; }
    public required string Text { get; set; }
    public int Points { get; set; }

    [ForeignKey(nameof(Section))]
    public int SectionId { get; set; }
    public TestSection Section { get; set; } = default!;

    public int? ParentQuestionId { get; set; }
    public Question? ParentQuestion { get; set; }

    public ICollection<Question>? SubQuestions { get; set; }
    public ICollection<Answer> Answers { get; set; } = default!;
}