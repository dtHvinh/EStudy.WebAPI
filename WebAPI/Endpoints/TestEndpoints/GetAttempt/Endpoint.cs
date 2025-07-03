using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.TestEndpoints.GetAttempt;

public class Endpoint(ApplicationDbContext context)
    : Endpoint<GetAttemptDetailsRequest, GetAttemptDetailsResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{TestId}/attempts/{AttemptId}");
        Group<TestGroup>();
        Description(d => d.WithName("Get specific test attempt detail"));

    }

    public override async Task HandleAsync(GetAttemptDetailsRequest req, CancellationToken ct)
    {
        var attempt = await _context.TestAttempts
            .Include(a => a.AnswerSelections)
            .ThenInclude(s => s.SelectedAnswer)
            .FirstOrDefaultAsync(a => a.Id == req.AttemptId
                            && a.TestExamId == req.TestId, ct);

        if (attempt == null)
            await SendNotFoundAsync(ct);
        else if (attempt.UserId != int.Parse(this.RetrieveUserId()))
            await SendForbiddenAsync(ct);
        else
            await SendOkAsync(attempt.ToResponse(), ct);
    }
}