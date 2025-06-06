using FastEndpoints;
using WebAPI.Data;
using WebAPI.Endpoints.FlashCardSetEndpoints.AddSetToFavorite;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.RemoveSetFromFavorite;

public class Endpoint(ApplicationDbContext context) : Endpoint<AddSetToFavoriteRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{setId}/favorite");
        Group<FlashCardSetGroup>();
    }

    public override async Task HandleAsync(AddSetToFavoriteRequest req, CancellationToken ct)
    {
        var flashCardSet = await _context.FlashCardSets.FindAsync([req.SetId], ct);
        if (flashCardSet is null)
            ThrowError("Flash card set not found");

        flashCardSet.IsFavorite = false;
        _context.FlashCardSets.Update(flashCardSet);

        if (await _context.SaveChangesAsync(ct) == 1)
            await SendOkAsync(ct);

        else
            ThrowError("Failed to remove flash card set to favorite");
    }
}