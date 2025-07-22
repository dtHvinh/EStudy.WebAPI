using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models._course;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.MarkLessonAsCompleted;

public class Endpoint(ApplicationDbContext context) : Endpoint<MarkLessonAsCompletedRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("lessons/{LessonId}/completed");
        Description(x => x.WithName("Mark Lesson As Completed").WithSummary("Marks a lesson as completed for a specific course."));
        Group<CourseGroup>();
    }
    public override async Task HandleAsync(MarkLessonAsCompletedRequest request, CancellationToken ct)
    {
        var lesson = await _context.Lessons
            .Where(e => e.Chapter.Course.Enrollments.Any(e => e.UserId == int.Parse(this.RetrieveUserId())))
            .FirstOrDefaultAsync(e => e.Id == request.LessonId, ct);

        if (lesson == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _context.LessonProgresses.Add(new LessonProgress
        {
            LessonId = request.LessonId,
            UserId = int.Parse(this.RetrieveUserId()),
            CompletionDate = DateTimeOffset.UtcNow,
        });

        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
