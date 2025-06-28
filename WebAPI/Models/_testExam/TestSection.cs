using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._testExam;

[Table("TestSections")]
public class TestSection
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }

    [ForeignKey(nameof(Test))]
    public int TestId { get; set; }
    public TestExam Test { get; set; } = default!;

    public ICollection<Question> Questions { get; set; } = default!;
}
