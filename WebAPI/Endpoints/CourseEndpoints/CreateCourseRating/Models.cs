namespace WebAPI.Endpoints.CourseEndpoints.CreateCourseRating;

public sealed class CreateCourseRatingRequest
{
    public int CourseId { get; set; }
    public int Value { get; set; } // Rating value, e.g., 1-5 stars
    public string? Review { get; set; } // Optional review text
}
