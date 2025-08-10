using FastEndpoints;
using WebAPI.Data;
using WebAPI.Models._ai;

namespace WebAPI.Endpoints.AIEndpoints.SaveConversationMessage;

public class Endpoint(ApplicationDbContext context) : Endpoint<SaveConversationMessageRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("conversations/{ConversationId}/messages");
        AllowAnonymous();
        Description(x => x
            .WithName("Save Conversation Message")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest));
        Group<AIGroup>();
    }

    public override async Task HandleAsync(SaveConversationMessageRequest req, CancellationToken ct)
    {
        var newMessage = new ConversationMessage
        {
            ConversationId = req.ConversationId,
            Message = req.Message,
            IsUserMessage = req.IsUserMessage,
            Timestamp = DateTimeOffset.UtcNow
        };

        _context.ConversationMessages.Add(newMessage);

        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
