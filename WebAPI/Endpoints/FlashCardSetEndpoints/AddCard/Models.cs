namespace WebAPI.Endpoints.FlashCardSetEndpoints.AddCard;

public sealed class AddCardRequest
{
    public int SetId { get; set; }
    public string Front { get; set; } = default!;
    public string Back { get; set; } = default!;
}