using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.GetCourseRatings;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetCourseRatingsRequest, List<GetCourseRatingsResponse>>
{
    private readonly ApplicationDbContext _context = context;
    public override void Configure()
    {
        Get("{CourseId}/ratings");
        Description(x => x
            .WithName("Get Course Ratings")
            .Produces<List<GetCourseRatingsResponse>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithTags("Courses"));
        Group<CourseGroup>();
    }
    public override async Task HandleAsync(GetCourseRatingsRequest request, CancellationToken ct)
    {
        var res = await _context.CourseRatings
            .Where(e => e.CourseId == request.CourseId && e.IsVisible)
            .OrderByDescending(e => e.CreationDate)
            .Paginate(request.Page, request.PageSize)
            .Select(e => new GetCourseRatingsResponse
            {
                AverageRating = e.Course.Ratings.Select(e => e.Value).DefaultIfEmpty().Average(),
                Id = e.Id,
                UserName = e.User.Name,
                UserProfilePicture = e.User.ProfilePicture,
                Value = e.Value,
                Review = e.Review,
                CreationDate = e.CreationDate,
            })
            .ToListAsync(ct);

        await SendOkAsync(res, ct);
    }
}
