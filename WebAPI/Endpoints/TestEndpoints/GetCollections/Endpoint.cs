using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.TestEndpoints.GetCollections;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetCollectionsRequest, List<GetCollectionsResponse>>
{
    private readonly ApplicationDbContext _context = context;
    public override void Configure()
    {
        Get("/test-collections");
        AllowAnonymous();
        Description(x => x
            .WithSummary("Get all test collections")
            .WithDescription("This endpoint retrieves all test collections.")
            .Produces(StatusCodes.Status200OK));
        Group<TestGroup>();
    }
    public override async Task HandleAsync(GetCollectionsRequest request, CancellationToken ct)
    {
        var query = Query<string>("query", false);

        var q = _context.TestCollections
            .Paginate(request.Page, request.PageSize);

        if (!string.IsNullOrWhiteSpace(query))
            q = q.Where(c => c.SearchVector.Matches(EF.Functions.ToTsQuery(query)));

        var res = await q
            .OrderByDescending(e => e.Id)
            .Select(c => new GetCollectionsResponse
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                TestCount = c.Tests.Count
            })
            .ToListAsync(ct);

        await SendOkAsync(res, ct);
    }
}
