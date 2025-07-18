using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.GetEnrolledCourse;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetEnrolledCourseRequest>
{
    private readonly ApplicationDbContext _context = context;
    public override void Configure()
    {
        Get("enrolled");
        Description(x => x.WithName("Get Enrolled Courses").WithSummary("Retrieves a list of courses the user is enrolled in."));
        Group<CourseGroup>();
    }
    public override async Task HandleAsync(GetEnrolledCourseRequest request, CancellationToken ct)
    {
        var userId = int.Parse(this.RetrieveUserId() ?? throw new InvalidOperationException("User ID not found."));
        var query = _context.Courses
            .Where(c => c.Enrollments.Any(e => e.UserId == userId))
            .AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            query = query.Where(c => c.Title.Contains(request.Query) || c.Description.Contains(request.Query));
        }

        var courses = await query
            .Paginate(request.Page, request.PageSize)
            .Select(e => new GetEnrolledCourseResponse
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                ImageUrl = e.ImageUrl,
                CompletionPercentage = e.Chapters.SelectMany(ch => ch.Lessons).Any()
                    ? ((float)_context.LessonProgresses.Count(lp =>
                        lp.UserId == userId &&
                        e.Chapters.SelectMany(ch => ch.Lessons).Select(l => l.Id).Contains(lp.LessonId)
                      ) / e.Chapters.SelectMany(ch => ch.Lessons).Count() * 100.0f)
                    : 0f // TODO: Debug this one, seem not working properly
            })
            .ToListAsync(ct);

        await SendOkAsync(courses, ct);
    }
}
