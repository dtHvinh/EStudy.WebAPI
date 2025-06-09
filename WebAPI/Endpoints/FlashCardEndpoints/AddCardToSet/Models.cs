namespace WebAPI.Endpoints.FlashCardEndpoints.AddCardToSet;

public sealed class AddCardRequest
{
    public int SetId { get; set; }
    public string Term { get; set; } = default!;
    public string Definition { get; set; } = default!;
    public string? PartOfSpeech { get; set; }
    public string? Example { get; set; }
    public string? Note { get; set; }
    public IFormFile? Image { get; set; }
}

public sealed class AddCardResponse
{
    public int Id { get; set; }
    public string Term { get; set; } = default!;
    public string Definition { get; set; } = default!;
    public string? PartOfSpeech { get; set; }
    public string? Example { get; set; }
    public string? Note { get; set; }
    public string? ImageUrl { get; set; }
}