using FastEndpoints;
using WebAPI.Data;
using WebAPI.Models._ai;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.AIEndpoints.CreateConversation;

public class Endpoint(ApplicationDbContext context) : Endpoint<CreateConversationRequest, int>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("conversations");
        AllowAnonymous();
        Description(x => x
            .WithName("Create Conversation")
            .Produces<int>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest));
        Group<AIGroup>();
    }

    public override async Task HandleAsync(CreateConversationRequest req, CancellationToken ct)
    {
        var newConversation = new Conversation
        {
            Name = req.Name,
            Description = req.Description,
            Context = req.Context,
            CreationDate = DateTime.UtcNow,
            UserId = int.Parse(this.RetrieveUserId()),
        };

        _context.Conversations.Add(newConversation);

        await _context.SaveChangesAsync(ct);

        await SendOkAsync(newConversation.Id, ct);
    }
}
