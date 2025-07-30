using FastEndpoints;
using WebAPI.AI.Contract;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.AIEndpoints.TextChat;

public sealed class Endpoint(IOllamaEnglishAssistant assistant) : Endpoint<TextChatRequest>
{
    private readonly IOllamaEnglishAssistant _assistant = assistant;

    public override void Configure()
    {
        Post("/text-chat");
        AllowAnonymous();
        Description(x => x
            .WithName("Text Chat")
            .Produces<string>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Generates a text response based on the provided message.")
            .WithDescription("This endpoint allows you to send a message and receive a generated text response."));
        Group<AIGroup>();
    }
    public override async Task HandleAsync(TextChatRequest req, CancellationToken ct)
    {
        HttpContext.StartStreaming();

        var stream = _assistant.CompleteAsync(req.Messages.ToMessageList(), cancelationToken: ct);

        await foreach (var token in stream)
        {
            if (token is not null && !string.IsNullOrEmpty(token.Message.Content))
            {
                await HttpContext.SendMessageAsync(token.Message.Content, ct);
            }
        }
    }
}


