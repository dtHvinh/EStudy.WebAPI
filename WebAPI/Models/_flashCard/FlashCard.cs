using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._flashCard;

[Table("FlashCards")]
public class FlashCard
{
    public int Id { get; set; }
    public required string Term { get; set; }
    public required string Definition { get; set; }

    [ForeignKey(nameof(FlashCard))]
    public int FlashCardSetId { get; set; }
    public FlashCardSet FlashCardSet { get; set; } = default!;
}
