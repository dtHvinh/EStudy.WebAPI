namespace WebAPI.Endpoints.AIEndpoints.TextChat;

public sealed class TextChatRequest
{
    public List<MessageRequest> Messages { get; init; } = default!;
}

public sealed class MessageRequest
{
    public string Role { get; init; } = default!;
    public string Content { get; init; } = default!;
}
