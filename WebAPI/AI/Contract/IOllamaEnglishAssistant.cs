using OllamaSharp.Models.Chat;
using WebAPI.AI.Builder;

namespace WebAPI.AI.Contract;

public interface IOllamaEnglishAssistant
{
    IAsyncEnumerable<ChatResponseStream?> CompleteAsync(List<Message> messages, EnglishAssistantOptions? options = null, CancellationToken cancelationToken = default);
}
