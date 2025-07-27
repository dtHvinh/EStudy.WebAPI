using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.GetMyCourseRating;

public class Endpoint(ApplicationDbContext context) : Endpoint<GetMyCourseRatingRequest, GetMyCourseRatingResponse>
{
    private readonly ApplicationDbContext _context = context;
    public override void Configure()
    {
        Get("{CourseId}/ratings/my");
        Description(d => d
            .WithName("Get My Course Rating")
            .WithDescription("Retrieve the rating and review given by the user for a specific course")
            .Produces<GetMyCourseRatingResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags("Courses"));
        Group<CourseGroup>();
    }
    public override async Task HandleAsync(GetMyCourseRatingRequest request, CancellationToken ct)
    {
        var userId = int.Parse(this.RetrieveUserId());
        var rating = await _context.CourseRatings
            .Where(e => e.CourseId == request.CourseId && e.UserId == userId)
            .Select(e => new GetMyCourseRatingResponse
            {
                Id = e.Id,
                Value = e.Value,
                Review = e.Review,
                CreationDate = e.CreationDate,
                UserName = e.User.Name,
                UserProfilePicture = e.User.ProfilePicture
            })
            .FirstOrDefaultAsync(ct);
        if (rating == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        await SendOkAsync(rating, ct);
    }
}
