using FastEndpoints;
using WebAPI.Data;

namespace WebAPI.Endpoints.WordEndpoints.DeleteWord;

public class Endpoint(ApplicationDbContext context) : EndpointWithoutRequest
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{id:int}");
        Group<WordGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");
        var w = await _context.Words.FindAsync([id], ct);
        if (w == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        _context.Words.Remove(w);
        await _context.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }
}
