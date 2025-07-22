using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.TestEndpoints.GetCollectionWithAddStatus;

public sealed class GetCollectionWithAddStatusRequest : PaginationParams
{
    public int TestId { get; set; }
}
public sealed class GetCollectionWithAddStatusResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public bool IsAdded { get; set; } = default!;
}
