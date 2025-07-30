using FastEndpoints;
using WebAPI.AI.Contract;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.AIEndpoints.Definition;

public class Endpoint(IOllamaEnglishAssistant assistant) : Endpoint<DefinitionRequest>
{
    private readonly IOllamaEnglishAssistant _assistant = assistant;

    public override void Configure()
    {
        Post("definition");
        AllowAnonymous();
        Group<AIGroup>();
    }
    public override async Task HandleAsync(DefinitionRequest req, CancellationToken ct)
    {
        var stream = _assistant.DefinitionAsync(req.Word, ct);

        HttpContext.StartStreaming();

        await foreach (var token in stream)
        {
            if (token is not null && !string.IsNullOrEmpty(token.Response))
            {
                await HttpContext.SendMessageAsync(token.Response, ct);
            }
        }
    }
}
