using FastEndpoints;
using WebAPI.AI;

namespace WebAPI.Endpoints.AIEndpoints.EnhancePrompt;

public class Endpoint(OpenAIAssistant openAIAssistant) : Endpoint<EnhancePromptRequest, EnhancePromptResponse>
{
    private readonly OpenAIAssistant _openAIAssistant = openAIAssistant;

    public override void Configure()
    {
        Post("enhance-prompt");
        AllowAnonymous();
        Description(x => x
            .WithName("Enhance Prompt")
            .Produces<EnhancePromptResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest));
        Group<AIGroup>();
    }
    public override async Task HandleAsync(EnhancePromptRequest req, CancellationToken ct)
    {
        var enhancedPrompt = await _openAIAssistant.EnhancePromptAsync(req.Prompt);
        await SendOkAsync(new EnhancePromptResponse { Prompt = enhancedPrompt }, ct);
    }
}
