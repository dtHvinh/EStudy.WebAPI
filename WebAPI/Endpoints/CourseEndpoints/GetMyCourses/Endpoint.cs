using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.GetMyCourses;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetMyCourseRequest, List<GetMyCourseResponse>>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("mine");
        Group<CourseGroup>();
    }

    public override async Task HandleAsync(GetMyCourseRequest req, CancellationToken ct)
    {
        var course = await _context.Courses
            .Where(e => e.AuthorId == int.Parse(this.RetrieveUserId()))
            .OrderByDescending(e => !e.IsPublished)
            .OrderByDescending(e => e.CreationDate)
            .Skip(req.PageSize * (req.Page - 1))
            .Take(req.PageSize)
            .ProjectToMyCourseResponse()
            .ToListAsync(ct);

        await SendOkAsync(course, ct);
    }
}