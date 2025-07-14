using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.TestEndpoints.GetDetails;

public class Endpoint(ApplicationDbContext context)
    : Endpoint<GetTestDetailsRequest, GetTestDetailsResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("details/{Id}");
        Group<TestGroup>();
        Description(d => d
            .WithName("Get Test Details")
            .WithDescription("Get test details with comments and pagination")
            .Produces<GetTestDetailsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
    }

    public override async Task HandleAsync(GetTestDetailsRequest req, CancellationToken ct)
    {
        var cp = Query<int>("comment_page", false);
        var cps = Query<int>("comment_size", false);

        var res = await _context.TestExams
            .Include(e => e.Comments)
            .ThenInclude(e => e.Author)
            .ProjectToGetDetailsResponse(this.RetrieveUserId(),
                commentSkip: (cp - 1) * cps,
                commentTake: cps
            )
            .FirstOrDefaultAsync(e => e.Id == req.Id, ct);

        if (res is null)
        {
            await SendNotFoundAsync(ct);
        }
        else
            await SendOkAsync(res, ct);
    }
}
