using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.TestEndpoints.GetCollections;

public sealed class GetCollectionsRequest : PaginationParams
{
}

public sealed class GetCollectionsResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public int TestCount { get; set; } = 0;
}
