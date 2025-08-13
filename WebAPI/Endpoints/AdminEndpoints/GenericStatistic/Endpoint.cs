using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Endpoints.AdminEndpoints.GenericStatistic;

public class Endpoint(ApplicationDbContext context) : EndpointWithoutRequest<GenericStatisticResonse>
{
    private readonly ApplicationDbContext _context = context;
    public override void Configure()
    {
        Get("generic-statistics");
        Description(d => d
            .WithName("Get Generic Statistics")
            .WithDescription("Retrieve generic statistics for the application")
            .Produces<GenericStatisticResonse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags("Admin"));
        Group<AdminGroup>();
    }
    public override async Task HandleAsync(CancellationToken ct)
    {
        var totalUsers = await _context.Users.CountAsync(ct);
        var totalCourses = await _context.Courses.CountAsync(ct);
        var totalBlogs = await _context.Blogs.CountAsync(ct);
        var totalTests = await _context.TestExams.CountAsync(ct);
        var response = new GenericStatisticResonse
        {
            TotalUsers = totalUsers,
            TotalCourses = totalCourses,
            TotalBlogs = totalBlogs,
            TotalTests = totalTests
        };
        await SendOkAsync(response, ct);
    }
}
