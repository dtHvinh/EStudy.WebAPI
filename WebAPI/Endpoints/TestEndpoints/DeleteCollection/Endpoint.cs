using FastEndpoints;
using WebAPI.Data;

namespace WebAPI.Endpoints.TestEndpoints.DeleteCollection;

public class Endpoint(ApplicationDbContext context) : EndpointWithoutRequest
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("/test-collections/{Id}");
        AllowAnonymous();
        Description(x => x
            .WithSummary("Delete a test collection")
            .WithDescription("This endpoint deletes a test collection by its ID.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound));
        Group<TestGroup>();
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("Id", true);

        if (id <= 0)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var collection = await _context.TestCollections.FindAsync([id], ct);

        if (collection == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _context.TestCollections.Remove(collection);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
