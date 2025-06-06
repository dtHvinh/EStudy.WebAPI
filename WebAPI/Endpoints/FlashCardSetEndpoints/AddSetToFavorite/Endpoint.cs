using FastEndpoints;
using WebAPI.Data;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.AddSetToFavorite;

public class Endpoint(ApplicationDbContext context) : Endpoint<AddSetToFavoriteRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("{setId}/favorite");
        Group<FlashCardSetGroup>();
    }

    public override async Task HandleAsync(AddSetToFavoriteRequest req, CancellationToken ct)
    {
        var flashCardSet = await _context.FlashCardSets.FindAsync([req.SetId], ct);
        if (flashCardSet is null)
            ThrowError("Flash card set not found");

        flashCardSet.IsFavorite = true;
        _context.FlashCardSets.Update(flashCardSet);

        if (await _context.SaveChangesAsync(ct) == 1)
            await SendOkAsync(ct);

        else
            ThrowError("Failed to add flash card set to favorite");
    }
}
