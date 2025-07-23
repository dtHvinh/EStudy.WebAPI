using FastEndpoints;
using WebAPI.Data;

namespace WebAPI.Endpoints.TestEndpoints.UpdateTestCollection;

public class Endpoint(ApplicationDbContext context) : Endpoint<UpdateTestCollectionRequest>
{
    private readonly ApplicationDbContext _context = context;
    public override void Configure()
    {
        Put("/test-collections/{Id}");
        AllowAnonymous();
        Description(x => x
            .WithSummary("Update a test collection")
            .WithDescription("This endpoint updates an existing test collection.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound));
        Group<TestGroup>();
    }
    public override async Task HandleAsync(UpdateTestCollectionRequest request, CancellationToken ct)
    {
        var collection = await _context.TestCollections.FindAsync([request.Id], ct);
        if (collection == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        collection.Name = request.Name;
        collection.Description = request.Description;

        await _context.SaveChangesAsync(ct);
        await SendNoContentAsync(ct);
    }
}
