using FastEndpoints;
using WebAPI.Data;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.DeleteCardFromSet;

public class Endpoint(ApplicationDbContext context) : Endpoint<DeleteCardFromSetRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("/{setId}/remove/{cardId}");
        Group<FlashCardSetGroup>();
    }

    public override async Task HandleAsync(DeleteCardFromSetRequest req, CancellationToken ct)
    {
        var set = await _context.FlashCardSets.FindAsync([req.SetId], ct);

        if (set is null)
        {
            ThrowError("Set not found");
            return;
        }

        var card = await _context.FlashCards.FindAsync([req.CardId], ct);

        if (card is null)
        {
            ThrowError("Card not found");
            return;
        }

        set.FlashCards.Remove(card);

        if (await _context.SaveChangesAsync(ct) == 1)
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            ThrowError("Failed to remove card from set");
        }
    }
}
