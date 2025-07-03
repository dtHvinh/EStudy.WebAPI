using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.TestEndpoints.GetListAttemptPreview;

public class Endpoint(ApplicationDbContext context)
    : Endpoint<GetListAttemptPreviewRequest, List<GetListAttemptPreviewResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{TestId}/attempts");
        Group<TestGroup>();
        Description(d => d.WithName("Get test attempts"));

    }

    public override async Task HandleAsync(GetListAttemptPreviewRequest req, CancellationToken ct)
    {
        var testAttemps = await _context.TestAttempts
            .Where(e => e.TestExamId == req.TestId
                     && e.UserId == int.Parse(this.RetrieveUserId()))
            .OrderByDescending(e => e.EarnedPoints)
            .ThenByDescending(e => e.CreationDate)
            .Skip((req.Page - 1) * req.PageSize)
            .Take(req.PageSize)
            .Select(e => new GetListAttemptPreviewResponse()
            {
                Id = e.Id,
                SubmitDate = e.CreationDate,
                TimeSpent = e.TimeSpent,
                EarnedPoints = e.EarnedPoints
            })
            .ToListAsync(ct);

        await SendOkAsync(testAttemps, ct);
    }
}
