using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;
using WebAPI.Models.Contract;

namespace WebAPI.Models._testExam;

[Table("TestComments")]
public class TestComment : IBelongToUser<int>
{
    public int Id { get; set; }
    public string Comment { get; set; } = default!;

    [ForeignKey(nameof(Test))]
    public int TestId { get; set; }
    public TestExam Test { get; set; } = default!;

    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    public User Author { get; set; } = default!;
}
