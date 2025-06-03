using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._others;

[Table("FlashCards")]
public class FlashCard
{
    public int Id { get; set; }
    public required string Front { get; set; }
    public required string Back { get; set; }

    [ForeignKey(nameof(FlashCard))]
    public int FlashCardSetId { get; set; }
    public FlashCardSet FlashCardSet { get; set; } = default!;
}
