using OllamaSharp.Models.Chat;
using WebAPI.AI.Extensions;

namespace WebAPI.AI.Builder;

public class RequestBuilder
{
    private bool? Think { get; set; } = EnglishAssistantOptions.Default.Think;
    private bool Stream { get; set; } = EnglishAssistantOptions.Default.Stream;
    private List<Message>? Messages { get; set; }
    private string? SystemPrompt { get; set; } = null;

    private RequestBuilder() { }

    public static RequestBuilder Create()
    {
        return new RequestBuilder();
    }

    public RequestBuilder WithOptions(EnglishAssistantOptions? options)
    {
        options ??= EnglishAssistantOptions.Default;
        Think = options.Think;
        Stream = options.Stream;
        return this;
    }

    public RequestBuilder WithMessages(List<Message> messages)
    {
        ArgumentNullException.ThrowIfNull(messages, nameof(messages));
        Messages = messages;
        return this;
    }

    public RequestBuilder WithSystemPrompt(string systemPrompt)
    {
        ArgumentNullException.ThrowIfNull(systemPrompt, nameof(systemPrompt));
        SystemPrompt = systemPrompt;
        return this;
    }

    public RequestBuilder WithStreaming()
    {
        Stream = true;
        return this;
    }

    public RequestBuilder WithoutThinking()
    {
        Think = false;
        return this;
    }

    public RequestBuilder WithThinking()
    {
        Think = true;
        return this;
    }

    public RequestBuilder WithoutStreaming()
    {
        Stream = false;
        return this;
    }

    public ChatRequest BuildChatRequest()
    {
        if (Messages is null || Messages.Count == 0)
        {
            throw new ArgumentException("Messages cannot be null or empty.", nameof(Messages));
        }

        var messages = SystemPrompt is null ? Messages : Messages.WithSysPrompt(SystemPrompt);

        return new ChatRequest
        {
            Messages = messages,
            Think = Think,
            Stream = Stream,
        };
    }
}

public sealed class EnglishAssistantOptions
{
    public bool? Think { get; set; } = true;
    public bool Stream { get; set; } = true;

    public static EnglishAssistantOptions Default => new()
    {
        Think = false,
        Stream = true
    };
}