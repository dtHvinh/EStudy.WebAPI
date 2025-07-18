namespace WebAPI.Endpoints.CourseEndpoints.GetMyCourseRating;

public sealed class GetMyCourseRatingRequest
{
    public int CourseId { get; set; }
}

public sealed class GetMyCourseRatingResponse
{
    public int Id { get; set; }
    public double Value { get; set; }
    public string? Review { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public string UserName { get; set; } = default!;
    public string? UserProfilePicture { get; set; } = default!;
}
