using FastEndpoints;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.EnrollFree;

public class Endpoint(ApplicationDbContext context) : Endpoint<EnrollFreeRequest>
{
    private readonly ApplicationDbContext _context = context;
    public override void Configure()
    {
        Post("{CourseId}/enroll-free");
        Group<CourseGroup>();
    }
    public override async Task HandleAsync(EnrollFreeRequest req, CancellationToken ct)
    {
        var course = await _context.Courses.FindAsync([req.CourseId], ct);

        if (course is null)
        {
            ThrowError("Course not found");
            return;
        }

        if (course.Price > 0)
        {
            ThrowError("This course is not free");
            return;
        }

        _context.CourseEnrollments.Add(new()
        {
            CourseId = req.CourseId,
            UserId = int.Parse(this.RetrieveUserId()),
            EnrollmentDate = DateTimeOffset.UtcNow
        });

        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}

