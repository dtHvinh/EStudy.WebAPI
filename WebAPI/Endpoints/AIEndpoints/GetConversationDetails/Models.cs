namespace WebAPI.Endpoints.AIEndpoints.GetConversationDetails;

public sealed class GetConversationDetailsRequest
{
    public int ConversationId { get; set; }
}

public sealed class GetConversationDetailsResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Context { get; set; } = string.Empty;
}