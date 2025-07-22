using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.TestEndpoints.AddOrRemoveTestInCollection;

public class Endpoint(ApplicationDbContext context) : FastEndpoints.Endpoint<AddTestToCollectionRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("/test-collections/add-test");
        AllowAnonymous();
        Description(x => x
            .WithSummary("Add test to collection")
            .WithDescription("This endpoint adds a test to a specified collection.")
            .Produces(StatusCodes.Status200OK));
        Group<TestGroup>();
    }
    public override async Task HandleAsync(AddTestToCollectionRequest request, CancellationToken ct)
    {
        var collection = await _context.TestCollections
            .Include(c => c.Tests)
            .FirstOrDefaultAsync(c => c.Id == request.CollectionId, ct);

        if (collection == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var test = await _context.TestExams
            .FirstOrDefaultAsync(t => t.Id == request.TestId, ct);

        if (test == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (request.IsAdd)
        {
            collection.Tests.Add(test);
        }
        else
        {
            collection.Tests.Remove(test);
        }

        _context.TestCollections.Update(collection);
        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
