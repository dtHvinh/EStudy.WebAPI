using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.Contract;

namespace WebAPI.Models._others;

[Table("FlashCardSets")]
public class FlashCardSet : IBelongToUser<int>
{
    public int Id { get; set; }
    public required string Title { get; set; }

    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    public User Author { get; set; } = default!;

    public ICollection<FlashCard> FlashCards { get; set; } = default!;

}
