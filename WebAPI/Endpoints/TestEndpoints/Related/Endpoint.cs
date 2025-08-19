using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.TestEndpoints.Related;

public class Endpoint(ApplicationDbContext context)
    : Endpoint<RelatedTestRequest, List<RelatedTestResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{TestId}/related");
        Group<TestGroup>();
        Description(d => d
            .WithName("Get Related Tests")
            .WithDescription("Get list of tests related to the test with given id using full-text search")
            .Produces<List<RelatedTestResponse>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
    }

    public override async Task HandleAsync(RelatedTestRequest req, CancellationToken ct)
    {
        var test = await _context.TestExams.FindAsync([req.TestId], ct);
        if (test is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // using "|" as separator to transform title into a format suitable for full-text
        // search 
        var f = test.Title.Replace(" ", " | ");

        List<RelatedTestResponse> relatedTests;

        try
        {
            relatedTests = await _context.TestExams
                .Where(e => e.Id != req.TestId)
                .OrderByDescending(e => e.SearchVector.Rank(
                    EF.Functions.ToTsQuery(f)))
                .Take(3)
                .ProjectToRelatedTestResponse()
                .ToListAsync(ct);
        }
        catch
        {
            relatedTests = [];
        }

        await SendOkAsync(relatedTests, ct);
    }
}