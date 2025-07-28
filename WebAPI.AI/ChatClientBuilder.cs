using Microsoft.Extensions.AI;
using OllamaSharp;

namespace WebAPI.AI;

public class ChatClientBuilder
{
    private IChatClient? _chatClient;
    private string? _endpoint;
    private string? _model;

    protected ChatClientBuilder()
    {
    }

    public static ChatClientBuilder Create()
    {
        return new ChatClientBuilder();
    }

    public IChatClient Ollama
    {
        get
        {
            if (string.IsNullOrEmpty(_endpoint) || string.IsNullOrEmpty(_model))
                throw new InvalidOperationException("Endpoint and Model must be set for Ollama provider.");

            var chatClient = new OllamaApiClient(_endpoint, _model);

            return chatClient;
        }
    }

    public ChatClientBuilder WithParameters(ChatClientParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        ArgumentNullException.ThrowIfNull(parameters.Endpoint);
        ArgumentNullException.ThrowIfNull(parameters.Model);

        _endpoint = parameters.Endpoint;
        _model = parameters.Model;

        return this;
    }

    public ChatClientBuilder WithEndpoint(string endpoint)
    {
        _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
        return this;
    }

    public ChatClientBuilder WithModel(string model)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        return this;
    }

    public IChatClient Build(AIProviders provider)
    {
        switch (provider)
        {
            case AIProviders.Ollama:
                _chatClient = Ollama;
                return _chatClient;

            default:
                _chatClient = Ollama;
                return _chatClient;
        }
    }
}
