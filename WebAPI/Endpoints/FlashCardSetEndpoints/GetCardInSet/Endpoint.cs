using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;
using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetCardInSet;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetCardInSetRequest, PagedResponse<FlashCardResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{setId}/cards");
        Group<FlashCardSetGroup>();
    }

    public override async Task HandleAsync(GetCardInSetRequest req, CancellationToken ct)
    {
        var shouldShuffle = Query<bool>("shuffle", false);

        // The card in set seem not that security critical to check if the user is the author of the set
        // so skip the check.
        var cardSet = await _context.FlashCardSets
            .FirstOrDefaultAsync(e => e.Id == req.SetId, ct);

        if (cardSet == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var query = _context.Entry(cardSet)
            .Collection(e => e.FlashCards)
            .Query();

        if (shouldShuffle)
        {
            query = query.OrderBy(e => Guid.NewGuid());
        }

        var totalCount = await query.CountAsync(ct);

        var skip = (req.Page.EnsureLargerOrEqual(1) - 1) * req.PageSize.EnsureInRange(1, 50);

        var response = await query
                    .Skip(skip)
                    .Take(req.PageSize.EnsureInRange(1, 50))
                    .ProjectToResponse()
                    .ToListAsync(ct);

        await SendOkAsync(response.ToPagedResponse(req.Page, req.PageSize, totalCount), ct);
    }
}
