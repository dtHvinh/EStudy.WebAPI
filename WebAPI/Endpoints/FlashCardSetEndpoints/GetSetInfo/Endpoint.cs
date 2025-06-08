using FastEndpoints;
using WebAPI.Data;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetSetInfo;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetSetInfoRequest, GetSetInfoResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{setId}");
        Group<FlashCardSetGroup>();
    }

    public override async Task HandleAsync(GetSetInfoRequest req, CancellationToken ct)
    {
        var set = await _context.FlashCardSets.FindAsync([req.SetId], ct);
        if (set is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(set.ToInfoResponse(), ct);
    }
}
