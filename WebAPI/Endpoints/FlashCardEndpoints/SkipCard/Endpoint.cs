using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.FlashCardEndpoints.SkipCard;

public class Endpoint(ApplicationDbContext context) : EndpointWithoutRequest
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Patch("skip");
        AllowAnonymous();
        Group<FlashCardGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Query<int>("id");

        var card = await _context.FlashCards
            .Include(e => e.FlashCardSet)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (card is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        card.IsSkipped = true;
        card.FlashCardSet.LastAccess = DateTimeOffset.UtcNow;

        var res = await _context.SaveChangesAsync(ct);

        if (res > 0)
            await SendNoContentAsync(ct);
        else
            await SendNotFoundAsync(ct);
    }
}
