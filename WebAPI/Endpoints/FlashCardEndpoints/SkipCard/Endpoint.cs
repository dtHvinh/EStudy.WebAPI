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

        var res = await _context.FlashCards
            .Where(e => e.Id == id)
            .ExecuteUpdateAsync(e => e.SetProperty(e => e.IsSkipped, true), ct);

        if (res > 0)
            await SendNoContentAsync(ct);
        else
            await SendNotFoundAsync(ct);
    }
}
