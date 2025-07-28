using Microsoft.Extensions.AI;
using WebAPI.AI.Contract;
using WebAPI.AI.Extensions;
using WebAPI.AI.Utils;

namespace WebAPI.AI;

public class ApplicationChatClient : IApplicationChatClient
{
    private readonly IChatClient _chatClient;
    private readonly ChatClientParameters _params;
    private readonly AIProviders _provider;

    public ApplicationChatClient(AIProviders provider, ChatClientParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        ArgumentNullException.ThrowIfNull(parameters.Endpoint);
        ArgumentNullException.ThrowIfNull(parameters.Model);

        _params = parameters;
        _provider = provider;
        _chatClient = ChatClientBuilder.Create()
           .WithParameters(parameters)
           .Build(provider);
    }

    public TClient GetClient<TClient>()
    {
        if (_provider == AIProviders.Ollama)
            return _chatClient.OfType<TClient>();

        throw new InvalidOperationException(EM.ChatClientNotType(typeof(TClient)));
    }
}


