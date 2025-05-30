namespace WebAPI.Models;

public class FlashCardSet
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<FlashCard> FlashCards { get; set; }
}
