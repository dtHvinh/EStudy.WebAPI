using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;
using WebAPI.Models.Contract;

namespace WebAPI.Models._testExam;

[Table("TestAttempts")]
public class TestAttempt : IEntityWithTime<int>
{
    public int Id { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    [ForeignKey(nameof(TestExam))]
    public int TestExamId { get; set; }
    public TestExam TestExam { get; set; } = default!;

    public DateTimeOffset CreationDate { get; set; }
    public double? Score { get; set; }

    public ICollection<TestAnswerSellection> AnswerSelections { get; set; } = default!;
}
