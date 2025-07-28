
namespace WebAPI.AI.Contract;

public interface IAIConfigurationService
{
    string GetProviderEndpoint(AIProviders provider);
    string GetSystemPrompt(AIProviders provider, string model, string context);
    string GetUrl(AIProviders provider);
}
