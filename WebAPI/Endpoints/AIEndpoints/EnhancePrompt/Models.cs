namespace WebAPI.Endpoints.AIEndpoints.EnhancePrompt;

public sealed class EnhancePromptRequest
{
    public string Prompt { get; set; } = string.Empty;
}

public sealed class EnhancePromptResponse
{
    public string Prompt { get; set; } = string.Empty;
}
