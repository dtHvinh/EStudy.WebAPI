using FastEndpoints;
using WebAPI.Constants;
using WebAPI.Data;
using WebAPI.Services.Contract;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.TestEndpoints.DeleteTest;

public class Endpoint(ApplicationDbContext context, ICurrentUserService currentUser) : EndpointWithoutRequest
{
    private readonly ApplicationDbContext _context = context;
    private readonly ICurrentUserService _currentUser = currentUser;

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

        if (await _currentUser.IsAdminAsync())
        {
            _context.TestExams.Remove(test);
            await _context.SaveChangesAsync(ct);

            await SendNoContentAsync(ct);
            return;
        }
        else if (await _currentUser.IsInRoleAsync(R.Instructor))
        {
            if (test.CreatorId != int.Parse(this.RetrieveUserId()))
            {
                await SendForbiddenAsync(ct);
                return;
            }

            _context.TestExams.Remove(test);
            await _context.SaveChangesAsync(ct);

            await SendNoContentAsync(ct);
            return;
        }

        ThrowError("You are not authorized to delete this test");
        return;
    }
}
