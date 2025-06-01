using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.DeleteSet;

public class Endpoint(ApplicationDbContext context) : Endpoint<DeleteSetRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{id}");
        Group<FlashCardSetGroup>();
    }

    public override async Task HandleAsync(DeleteSetRequest req, CancellationToken ct)
    {
        var set = await _context.FlashCardSets.Include(e => e.Author).FirstOrDefaultAsync(e => e.Id == req.Id, ct);

        if (set is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (!set.IsBelongTo(this.RetrieveUserId()))
        {
            await SendForbiddenAsync(ct);
        }

        _context.FlashCardSets.Remove(set);

        if (await _context.SaveChangesAsync(ct) == 1)
            await SendNoContentAsync(ct);

        ThrowError("Unable to delete set");
    }
}
