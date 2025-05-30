namespace WebAPI.Models;

public class FlashCard
{
    public int Id { get; set; }
    public string Front { get; set; }
    public string Back { get; set; }
    public int FlashCardSetId { get; set; }
    public FlashCardSet FlashCardSet { get; set; }
}
