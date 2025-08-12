using System.Diagnostics;
using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.AIEndpoints.GetConversationMessageList;

public sealed class GetConversationMessageListRequest : PaginationParams
{
    public int ConversationId { get; set; }
}

[DebuggerDisplay("Id = {Id}")]
public sealed class GetConversationMessageListResponse
{
    public int Id { get; set; }
    public string Message { get; set; } = default!;
    public string Role { get; set; } = default!;
    public DateTimeOffset Timestamp { get; set; }
}