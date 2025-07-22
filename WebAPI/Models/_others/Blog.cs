using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.Contract;

namespace WebAPI.Models._others;

[Table("Blogs")]
public class Blog : IEntityWithTime<int>, IBelongToUser<int>, IContentValidatable
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset? ModificationDate { get; set; }
    public bool IsHidden { get; set; }

    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    public User Author { get; set; } = default!;
    public NpgsqlTsVector SearchVector { get; set; } = default!;
}
