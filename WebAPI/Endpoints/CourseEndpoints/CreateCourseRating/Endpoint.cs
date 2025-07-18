using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models._course;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.CreateCourseRating;

public class Endpoint(ApplicationDbContext context) : Endpoint<CreateCourseRatingRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("{CourseId}/ratings");
        AllowAnonymous();
        Description(x => x.WithName("CreateOrUpdateCourseRating").WithSummary("Creates or updates a new rating for a specific course."));
        Group<CourseGroup>();
    }
    public override async Task HandleAsync(CreateCourseRatingRequest request, CancellationToken ct)
    {
        var rating = await _context.CourseRatings
            .FirstOrDefaultAsync(e => e.CourseId == request.CourseId && e.UserId == int.Parse(this.RetrieveUserId()), ct);

        if (rating == null)
        {
            _context.CourseRatings.Add(new CourseRating
            {
                CourseId = request.CourseId,
                UserId = int.Parse(this.RetrieveUserId()),
                Value = request.Value,
                Review = request.Review,
                CreationDate = DateTimeOffset.UtcNow,
            });
        }
        else
        {
            rating.Value = request.Value;
            rating.Review = request.Review;
            rating.ModificationDate = DateTimeOffset.UtcNow;

            _context.CourseRatings.Update(rating);
        }

        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
