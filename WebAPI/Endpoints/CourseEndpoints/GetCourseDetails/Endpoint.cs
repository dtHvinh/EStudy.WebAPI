using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.CourseEndpoints.GetCourseDetails;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetCourseDetailsRequest, GetCourseDetailsResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{CourseId}");
        Group<CourseGroup>();
    }

    public override async Task HandleAsync(GetCourseDetailsRequest req, CancellationToken ct)
    {
        var course = await _context.Courses.Where(c => c.Id == req.CourseId)
            .ProjectToCourseDetailsResponse()
            .FirstOrDefaultAsync(ct);

        if (course is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(course, ct);
    }
}