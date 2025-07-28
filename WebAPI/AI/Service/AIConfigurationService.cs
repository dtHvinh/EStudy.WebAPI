using WebAPI.AI.Contract;
using WebAPI.Utilities.Attributes;

namespace WebAPI.AI.Service;

class ModelContextSystemPrompts : Dictionary<string, string>;

[Service(ServiceLifetime.Singleton, typeof(IAIConfigurationService))]
public class AIConfigurationService(IConfiguration configuration) : IAIConfigurationService
{
    private readonly Dictionary<string, Dictionary<string, string>> _prompts = new(StringComparer.OrdinalIgnoreCase);
    private readonly IConfiguration _config = configuration;

    private string GetPromptsFilePath(AIProviders provider, string model, string context)
    {
        var configPath = $"AI:Settings:{provider}:Models:{model}:Prompts:{context}";
        if (_config[configPath] is string fileName && !string.IsNullOrWhiteSpace(fileName))
        {
            return fileName;
        }
        throw new FileNotFoundException($"Prompt file not found for {provider}/{model}/{context} at {configPath}");
    }

    public string GetProviderEndpoint(AIProviders provider)
    {
        var configPath = $"AI:Settings:{provider}:Endpoint";
        if (_config[configPath] is string endpoint && !string.IsNullOrWhiteSpace(endpoint))
        {
            return endpoint;
        }
        throw new NotSupportedException($"Provider {provider} is not supported or does not have an endpoint defined.");
    }

    public string GetUrl(AIProviders provider)
    {
        if (provider == AIProviders.Ollama)
            return "http://localhost:11434";
        throw new NotSupportedException($"Provider {provider} is not supported or does not have a URL defined.");
    }

    public string GetSystemPrompt(AIProviders provider, string model, string context)
    {
        var compositeKey = $"{provider}|{model}";
        if (_prompts.TryGetValue(compositeKey, out var store)
            && store.TryGetValue(context, out var prompt))
        {
            return prompt;
        }
        else
        {
            var filePath = GetPromptsFilePath(provider, model, context);
            var promptText = File.ReadAllText(filePath);

            var contextPrompts = new ModelContextSystemPrompts
            {
                { context, promptText }
            };

            _prompts.TryAdd(compositeKey, contextPrompts);
            return promptText;
        }

    }
}
