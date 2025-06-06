using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetUserSet;

public sealed class GetUserSetRequest : PaginationParams
{
}

public sealed class FlashCardSetResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTimeOffset? LastAccess { get; set; }
    public double Progress { get; set; }
    public bool IsFavorite { get; set; }
    public int CardCount { get; set; }
}
