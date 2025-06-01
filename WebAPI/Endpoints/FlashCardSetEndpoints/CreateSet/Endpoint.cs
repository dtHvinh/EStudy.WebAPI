using FastEndpoints;
using WebAPI.Data;
using WebAPI.Endpoints.FlashCardSetEndpoints.Create;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.CreateSet;

public class Endpoint(ApplicationDbContext context) : Endpoint<CreateFlashCardSetRequest, CreateFlashCardSetResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("");
        Group<FlashCardSetGroup>();
    }

    public override async Task HandleAsync(CreateFlashCardSetRequest req, CancellationToken ct)
    {
        var newFlashCardSet = req.ToFlashCardSet(this.RetrieveUserId());

        _context.FlashCardSets.Add(newFlashCardSet);

        if (await _context.SaveChangesAsync(ct) == 1)
            await SendOkAsync(newFlashCardSet.ToResponse(), ct);

        else
            ThrowError("Failed to create flash card set");
    }
}
