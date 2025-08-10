namespace WebAPI.Endpoints.AIEndpoints.SaveConversationMessage;

public sealed class SaveConversationMessageRequest
{
    public int ConversationId { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsUserMessage { get; set; }
}
