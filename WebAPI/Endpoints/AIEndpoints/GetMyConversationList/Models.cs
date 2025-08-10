using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.AIEndpoints.GetMyConversationList;

public sealed class GetMyConversationListRequest : PaginationParams
{
}

public sealed class GetMyConversationListResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Context { get; set; } = string.Empty;
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset LastActive { get; set; }
    public int MessageCount { get; set; }
}
