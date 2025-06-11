using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetCardInSet;

public sealed class GetCardInSetRequest : PaginationParams
{
    public int SetId { get; set; }
}

public sealed class FlashCardResponse
{
    public int Id { get; set; }
    public string Term { get; set; } = default!;
    public string Definition { get; set; } = default!;
    public string? Note { get; set; }
    public string? ImageUrl { get; set; }
    public string? Example { get; set; }
    public string? PartOfSpeech { get; set; }
}