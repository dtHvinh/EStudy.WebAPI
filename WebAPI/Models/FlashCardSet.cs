using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

[Table("FlashCardSets")]
public class FlashCardSet
{
    public int Id { get; set; }
    public required string Title { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public ICollection<FlashCard> FlashCards { get; set; } = default!;
}
