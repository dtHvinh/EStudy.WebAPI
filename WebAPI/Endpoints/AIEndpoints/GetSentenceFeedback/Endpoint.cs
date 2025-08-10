using FastEndpoints;
using WebAPI.AI;
using WebAPI.AI.JsonSchema;

namespace WebAPI.Endpoints.AIEndpoints.GetSentenceFeedback;

public class Endpoint(OpenAIAssistant assistant) : Endpoint<GetSentenceFeedbackRequest, GetSentenceFeedbackResponse>
{
    private readonly OpenAIAssistant _assistant = assistant;

    public override void Configure()
    {
        Post("feedback/sentence");
        AllowAnonymous();
        Description(x => x
            .WithName("Get sentence feedback")
            .WithSummary("Retrieves a sentence feedback for a sentences.")
            .Produces<SentenceFeedbackResponse>());
        Group<AIGroup>();
    }
    public override async Task HandleAsync(GetSentenceFeedbackRequest req, CancellationToken ct)
    {
        var response = await _assistant.GetSentenceFeedback(req.Sentence);
        await SendOkAsync(response.ToResponse(), ct);
    }
}
