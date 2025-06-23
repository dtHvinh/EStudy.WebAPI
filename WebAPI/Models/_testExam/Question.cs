using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._testExam;

public class Question
{
    public int Id { get; set; }
    public required string Text { get; set; }

    [ForeignKey(nameof(Section))]
    public int SectionId { get; set; }
    public TestSection Section { get; set; } = default!;

    public ICollection<Answer> Answers { get; set; } = default!;
}