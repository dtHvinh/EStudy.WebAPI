using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._flashCard;

[Table("FlashCards")]
[Index(nameof(Term), IsUnique = true)]
public class FlashCard
{
    public int Id { get; set; }
    public required string Term { get; set; }
    public required string Definition { get; set; }
    public string? PartOfSpeech { get; set; }
    public string? Example { get; set; }
    public string? Note { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsSkipped { get; set; }

    [ForeignKey(nameof(FlashCard))]
    public int FlashCardSetId { get; set; }
    public FlashCardSet FlashCardSet { get; set; } = default!;
    public NpgsqlTsVector SearchVector { get; set; } = default!;
}
