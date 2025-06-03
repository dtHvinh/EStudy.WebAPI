using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetCardInSet;

public sealed class GetCardInSetRequest : PaginationParams
{
    public int SetId { get; set; }
}

public sealed class FlashCardResponse
{
    public int Id { get; set; }
    public string Front { get; set; } = default!;
    public string Back { get; set; } = default!;
}