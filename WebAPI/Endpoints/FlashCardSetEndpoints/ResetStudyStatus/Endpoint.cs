using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.ResetStudyStatus;

public class Endpoint(ApplicationDbContext context) : Endpoint<ResetFlashCardRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Patch("{setId}/reset");
        Group<FlashCardSetGroup>();
    }

    public override async Task HandleAsync(ResetFlashCardRequest req, CancellationToken ct)
    {

        var res = await _context.FlashCards
            .Where(c => c.FlashCardSetId == req.SetId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(c => c.IsSkipped, false), ct);

        await SendOkAsync(ct);
    }
}

