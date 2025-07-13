using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.CourseEndpoints.GetCourses;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetCoursesRequest, List<GetCourseResponse>>
{
    public override void Configure()
    {
        Get("");
        AllowAnonymous();
        Description(x => x
            .WithName("GetCourses")
            .Produces<List<GetCourseResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound));
        Group<CourseGroup>();
    }
    public override async Task HandleAsync(GetCoursesRequest req, CancellationToken ct)
    {
        var nameQuery = Query<string>("name", false);

        var courses = context.Courses.Where(e => e.IsPublished);
        if (!string.IsNullOrEmpty(nameQuery))
        {
            courses = courses.Where(c => c.Title.Contains(nameQuery));
        }

        var response = await courses
            .Skip(req.PageSize * (req.Page - 1))
            .Take(req.PageSize)
            .ProjectToGetCoursesResponse()
            .ToListAsync(ct);

        await SendOkAsync(response, ct);
    }
}
