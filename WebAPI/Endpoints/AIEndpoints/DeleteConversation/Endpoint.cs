using FastEndpoints;
using WebAPI.Data;

namespace WebAPI.Endpoints.AIEndpoints.DeleteConversation;

public class Endpoint(ApplicationDbContext context) : Endpoint<DeleteConversationRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("conversations/{ConversationId}");
        Group<AIGroup>();
    }

    public override async Task HandleAsync(DeleteConversationRequest req, CancellationToken ct)
    {
        var conversation = await _context.Conversations.FindAsync([req.ConversationId], ct);

        if (conversation is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _context.Conversations.Remove(conversation);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
