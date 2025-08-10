using FastEndpoints;
using WebAPI.Data;

namespace WebAPI.Endpoints.AIEndpoints.GetConversationDetails;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetConversationDetailsRequest, GetConversationDetailsResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("conversations/{ConversationId}");
        AllowAnonymous();
        Description(x => x
            .WithName("Get Conversation Details")
            .Produces<GetConversationDetailsResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound));
        Group<AIGroup>();
    }
    public override async Task HandleAsync(GetConversationDetailsRequest req, CancellationToken ct)
    {
        var conversation = await _context.Conversations.FindAsync([req.ConversationId], cancellationToken: ct);
        if (conversation == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        try
        {
            conversation.LastActive = DateTimeOffset.UtcNow;
            _context.Conversations.Update(conversation);
            await _context.SaveChangesAsync(ct);
            await SendOkAsync(conversation.MapToResponse(), ct);
        }
        catch
        {
            ThrowError("An error occurred while retrieving the conversation details.");
        }
    }
}
