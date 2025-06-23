using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;
using WebAPI.Models.Contract;

namespace WebAPI.Models._testExam;

[Table("TestCollections")]
public class TestCollection : IBelongToUser<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;

    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    public User Author { get; set; } = default!;
}
