using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.Contract;

namespace WebAPI.Models._others;

[Table("Blogs")]
public class Blog : IEntityWithTime<int>, IBelongToUser<int>
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset? ModificationDate { get; set; }

    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    public User Author { get; set; } = default!;
}
