using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.GetCourseDetails;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetCourseDetailsRequest, GetCourseDetailsResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{CourseId}");
        Description(d => d
            .WithName("Get Course Details")
            .WithDescription("Retrieve detailed information about a specific course")
            .Produces<GetCourseDetailsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags("Courses"));
        Group<CourseGroup>();
    }

    public override async Task HandleAsync(GetCourseDetailsRequest req, CancellationToken ct)
    {
        var course = await _context.Courses.Where(c => c.Id == req.CourseId)
            .ProjectToCourseDetailsResponse(int.Parse(this.RetrieveUserId()))
            .FirstOrDefaultAsync(ct);

        if (course is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(course, ct);
    }
}