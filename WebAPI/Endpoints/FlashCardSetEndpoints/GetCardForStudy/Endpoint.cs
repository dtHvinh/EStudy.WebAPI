using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetCardForStudy;

public class Endpoint(ApplicationDbContext context)
    : Endpoint<GetCardForStudyRequest, List<FlashCardStudyResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{setId}/study");
        Group<FlashCardSetGroup>();
    }

    public override async Task HandleAsync(GetCardForStudyRequest req, CancellationToken ct)
    {
        var shuffle = Query<bool>("shuffle", false);

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

        if (shuffle)
            query = query.OrderBy(e => Guid.NewGuid());
        else
            query = query.OrderBy(e => e.Term);

        var response = await query
                    .Where(e => !e.IsSkipped)
                    .Skip((req.Page - 1) * req.PageSize)
                    .Take(req.PageSize)
                    .ProjectToStudyResponse()
                    .ToListAsync(ct);

        await SendOkAsync(response, ct);
    }
}