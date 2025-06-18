using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetCardInSet;

public class EndpointV2(ApplicationDbContext context) : Endpoint<GetCardInSetRequest, List<FlashCardResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{setId}/cards");
        Group<FlashCardSetGroup>();
    }

    public override async Task HandleAsync(GetCardInSetRequest req, CancellationToken ct)
    {
        var searchTerm = Query<string>("q", false);

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

        if (!string.IsNullOrEmpty(searchTerm))
            query = query.Where(e => e.SearchVector.Matches(EF.Functions.PhraseToTsQuery(searchTerm)));

        var response = await query
                    .OrderBy(e => e.Term)
                    .Skip((req.Page - 1) * req.PageSize)
                    .Take(req.PageSize)
                    .ProjectToResponse()
                    .ToListAsync(ct);

        await SendOkAsync(response, ct);
    }
}