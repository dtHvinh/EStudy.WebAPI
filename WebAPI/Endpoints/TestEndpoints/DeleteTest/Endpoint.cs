using FastEndpoints;
using WebAPI.Data;

namespace WebAPI.Endpoints.TestEndpoints.DeleteTest;

public class Endpoint(ApplicationDbContext context) : EndpointWithoutRequest
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Delete("{id}");
        AllowAnonymous();
        Description(x => x
            .WithTags("Test")
            .WithSummary("Delete a test by ID")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError));
        Group<TestGroup>();
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id", true);

        if (id <= 0)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var test = await _context.TestExams.FindAsync(id, ct);

        if (test == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _context.TestExams.Remove(test);
        await _context.SaveChangesAsync(ct);

        await SendNoContentAsync(ct);
    }
}
