using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models._course;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.MarkAsLearned;

public class Endpoint(ApplicationDbContext context) : Endpoint<MarkAsLearnRequest, MarkAsLearnResponse>
{
    private readonly ApplicationDbContext _context = context;
    public override void Configure()
    {
        Post("{CourseId}/learned/{LessonId}");
        Description(x => x
            .WithName("Mark Lesson as Learned")
            .Produces<MarkAsLearnResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithDescription("Mark a lesson as learned"));
        Group<CourseGroup>();
    }
    public override async Task HandleAsync(MarkAsLearnRequest request, CancellationToken cancellationToken)
    {
        var isEnrolled = await _context.CourseEnrollments.AnyAsync(e => e.CourseId == request.CourseId && e.UserId == int.Parse(this.RetrieveUserId()), cancellationToken);
        if (!isEnrolled)
        {
            ThrowError("You are not enrolled in this course.");
            return;
        }
        var lesson = _context.Lessons.FirstOrDefault(e => e.Id == request.LessonId);
        if (lesson == null)
        {
            ThrowError("Lesson not found.");
            return;
        }

        var userId = int.Parse(this.RetrieveUserId());
        var lessonProgress = await _context.LessonProgresses
            .FirstOrDefaultAsync(e => e.LessonId == request.LessonId && e.UserId == userId, cancellationToken);

        if (lessonProgress == null)
        {
            lessonProgress = new LessonProgress
            {
                LessonId = request.LessonId,
                UserId = userId,
                CompletionDate = DateTimeOffset.UtcNow
            };
            _context.LessonProgresses.Add(lessonProgress);
        }
        else
        {
            lessonProgress.CompletionDate = DateTimeOffset.UtcNow;
        }

        await _context.SaveChangesAsync(cancellationToken);
        await SendOkAsync(new MarkAsLearnResponse(), cancellationToken);
    }
}
