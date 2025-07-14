using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.TestEndpoints.GetTest;

public class Endpoint(ApplicationDbContext context)
    : Endpoint<GetTestRequest, GetTestResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{TestId}");
        Group<TestGroup>();
        Description(d => d
            .WithName("Get Test")
            .WithDescription("Get test with questions and answers")
            .Produces<GetTestResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
    }

    public override async Task HandleAsync(GetTestRequest req, CancellationToken ct)
    {
        var test = await _context.TestExams
            .Include(e => e.Sections)
            .ThenInclude(e => e.Questions)
            .ThenInclude(e => e.Answers)
            .AsSplitQuery()
            .ProjectToResponse()
            .FirstOrDefaultAsync(t => t.Id == req.TestId, ct);

        if (test is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(test, ct);
    }
}
