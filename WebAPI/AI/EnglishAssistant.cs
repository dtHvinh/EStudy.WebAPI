using OllamaSharp;
using OllamaSharp.Models.Chat;
using WebAPI.AI.Builder;
using WebAPI.AI.Contract;
using WebAPI.Utilities.Attributes;

namespace WebAPI.AI;

[Service(ServiceLifetime.Singleton, typeof(IOllamaEnglishAssistant))]
public class EnglishAssistant(IAIConfigurationService provider) : IOllamaEnglishAssistant
{
    private const string Model = "qwen3:1.7b";

    private readonly OllamaApiClient _ollamaClient = new(provider.GetProviderEndpoint(AIProviders.Ollama), Model);
    private readonly IAIConfigurationService _promptProvider = provider;

    private OllamaApiClient Client => _ollamaClient;
    private string TutorSystemPrompt => _promptProvider.GetSystemPrompt(AIProviders.Ollama, Model, "Tutor");

    public IAsyncEnumerable<ChatResponseStream?> CompleteAsync(List<Message> messages, EnglishAssistantOptions? options = null, CancellationToken cancelationToken = default)
    {
        var request = RequestBuilder.Create()
            .WithMessages(messages)
            .WithSystemPrompt(TutorSystemPrompt)
            .WithoutThinking()
            .WithStreaming()
            .BuildChatRequest();

        return Client.ChatAsync(request, cancelationToken);
    }
}
