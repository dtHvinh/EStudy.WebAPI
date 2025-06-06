using FastEndpoints;
using WebAPI.Data;
using WebAPI.Endpoints.FlashCardSetEndpoints.UpdateSetName;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.UpdateSet;

public class Endpoint(ApplicationDbContext context) : Endpoint<UpdateSetRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("/{id}");
        Group<FlashCardSetGroup>();
        Description(d => d.WithName("UpdateFlashCardSet").WithDescription("Update the current user's flashcard sets"));
    }

    public override async Task HandleAsync(UpdateSetRequest req, CancellationToken ct)
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

        flashCardSet.UpdateSet(req);

        if (await _context.SaveChangesAsync(ct) == 1)
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            ThrowError("Unable to update flash card set");
        }
    }
}