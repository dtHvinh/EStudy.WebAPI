using FastEndpoints;
using WebAPI.AI;
using WebAPI.AI.JsonSchema;

namespace WebAPI.Endpoints.AIEndpoints.GetTranslation;

public class Endpoint(OpenAIAssistant assistant) : Endpoint<GetTranslationRequest, GetTranslationResponse>
{
    private readonly OpenAIAssistant _assistant = assistant;

    public override void Configure()
    {
        Post("translate");
        AllowAnonymous();
        Description(x => x
            .WithName("GetTranslation")
            .WithSummary("Retrieves a translation for a text.")
            .Produces<DictionaryEntryResponse>());
        Group<AIGroup>();
    }
    public override async Task HandleAsync(GetTranslationRequest req, CancellationToken ct)
    {
        var response = await _assistant.GetTranslationAsync(req.Context, req.Text, req.Language);
        await SendOkAsync(new()
        {
            TranslatedText = response.TranslatedText
        }, ct);
    }
}
