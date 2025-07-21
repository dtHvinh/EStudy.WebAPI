using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models._course;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.MarkQuizAsCompleted;

public class Endpoint(ApplicationDbContext context) : Endpoint<MarkQuizAsCompletedRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("quizzes/{QuizId}/completed");
        Description(x => x.WithName("Mark Quiz As Completed").WithSummary("Marks a quiz as completed for a specific course."));
        Group<CourseGroup>();
    }
    public override async Task HandleAsync(MarkQuizAsCompletedRequest request, CancellationToken ct)
    {
        var quiz = await _context.ChapterQuizzes
            .FirstOrDefaultAsync(e => e.Id == request.QuizId, ct);

        if (quiz == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _context.QuizProgresses.Add(new QuizProgress
        {
            QuizId = request.QuizId,
            UserId = int.Parse(this.RetrieveUserId()),
            CompletionDate = DateTimeOffset.UtcNow,
        });

        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
