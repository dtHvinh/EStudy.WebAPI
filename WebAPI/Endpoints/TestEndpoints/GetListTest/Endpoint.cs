using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.TestEndpoints.GetListTest;

public class Endpoint(ApplicationDbContext context)
    : Endpoint<GetListTestRequest, List<GetListTestResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("");
        Group<TestGroup>();
        Description(d => d.WithName("Get test").WithDescription("Get list of test with pagination"));
    }

    public override async Task HandleAsync(GetListTestRequest req, CancellationToken ct)
    {
        var q = Query<string>("q", false);

        var query = _context.TestExams.AsQueryable();

        if (!string.IsNullOrEmpty(q))
            query = query.Where(x => x.SearchVector.Matches(q));

        var res = await query
            .OrderByDescending(x => x.Id)
            .Skip(req.PageSize * (req.Page - 1))
            .Take(req.PageSize)
            .ProjectToGetListTestResponse()
            .ToListAsync(ct);

        await SendOkAsync(res, ct);
    }
}