using FastEndpoints;
using WebAPI.AI;
using WebAPI.AI.JsonSchema;

namespace WebAPI.Endpoints.AIEndpoints.GetTerm;

public class Endpoint(OpenAIAssistant assistant) : Endpoint<GetTermRequest, DictionaryEntryResponse>
{
    private readonly OpenAIAssistant _assistant = assistant;

    public override void Configure()
    {
        Post("definition");
        AllowAnonymous();
        Description(x => x
            .WithName("GetTerm")
            .WithSummary("Retrieves a dictionary entry for a given term.")
            .Produces<DictionaryEntryResponse>());
        Group<AIGroup>();
    }
    public override async Task HandleAsync(GetTermRequest req, CancellationToken ct)
    {
        var response = await _assistant.GetDictionaryEntryAsync(req.Term);
        await SendOkAsync(response, ct);
    }
}
