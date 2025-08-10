using FastEndpoints;
using WebAPI.Data;
using WebAPI.Models._testExam;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.TestEndpoints.RecordAttempt;

public class Endpoint(ApplicationDbContext context)
    : Endpoint<RecordAttemptRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("{TestExamId}/attempts");
        Group<TestGroup>();
        Description(d => d.WithName("Record test attemp"));
    }

    public override async Task HandleAsync(RecordAttemptRequest req, CancellationToken ct)
    {
        var testExam = await _context.TestExams.FindAsync([req.TestExamId], ct);
        if (testExam is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var newAttemp = new TestAttempt()
        {
            TestExam = testExam,
            TimeSpent = req.TimeSpent,
            CreationDate = DateTimeOffset.UtcNow,
            UserId = int.Parse(this.RetrieveUserId()),
            EarnedPoints = req.EarnedPoints,
            AnswerSelections = [.. req.AnswerSelections
            .SelectMany(q =>
                q.SelectedAnswerIds.Select(answerId => new TestAnswerSelection
                {
                    QuestionId = q.QuestionId,
                    SelectedAnswerId = answerId,
                })
            )]
        };

        _context.TestAttempts.Add(newAttemp);

        if (await _context.SaveChangesAsync(ct) > 0)
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            ThrowError("Failed to record the attemp");
        }
    }
}
