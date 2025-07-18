using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.GetCourses;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetCoursesRequest, List<GetCourseResponse>>
{
    public override void Configure()
    {
        Get("");
        AllowAnonymous();
        Description(x => x
            .WithName("Get Courses")
            .WithDescription("Get paginated list of published courses with optional name filtering")
            .Produces<List<GetCourseResponse>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
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
            .ProjectToGetCoursesResponse(int.Parse(this.RetrieveUserId()))
            .ToListAsync(ct);

        await SendOkAsync(response, ct);
    }
}
