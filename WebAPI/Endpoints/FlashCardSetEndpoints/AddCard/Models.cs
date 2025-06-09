namespace WebAPI.Endpoints.FlashCardSetEndpoints.AddCard;

public sealed class AddCardRequest
{
    public int SetId { get; set; }
    public string Term { get; set; } = default!;
    public string Definition { get; set; } = default!;
}

public sealed class AddCardResponse
{
    public int Id { get; set; }
    public string Term { get; set; } = default!;
    public string Definition { get; set; } = default!;
}