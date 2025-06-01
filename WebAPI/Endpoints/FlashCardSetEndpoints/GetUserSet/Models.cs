using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetUserSet;

public sealed class GetUserSetRequest : PaginationParams
{
}

public sealed class FlashCardSetResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
}
