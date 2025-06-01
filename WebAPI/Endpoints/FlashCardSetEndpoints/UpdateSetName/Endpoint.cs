using FastEndpoints;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.UpdateSetName;

public class Endpoint(ApplicationDbContext context) : Endpoint<UpdateSetNameRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Patch("/{id}/name");
        Group<FlashCardSetGroup>();
        Description(d => d.WithName("UpdateFlashCardSetName").WithDescription("Update the current user's flashcard sets name"));
    }

    public override async Task HandleAsync(UpdateSetNameRequest req, CancellationToken ct)
    {
        var userId = this.RetrieveUserId();

        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var _))
        {
            ThrowError("User not authenticated or invalid user ID");
            return;
        }

        var flashCardSet = await _context.FlashCardSets.FindAsync([req.Id], ct);

        if (flashCardSet == null)
        {
            ThrowError("Flash card set not found");
            return;
        }

        flashCardSet.UpdateSetName(req);

        if (await _context.SaveChangesAsync(ct) == 1)
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            ThrowError("Unable to update flash card set name");
        }
    }
}