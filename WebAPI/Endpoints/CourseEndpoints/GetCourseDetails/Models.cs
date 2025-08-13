namespace WebAPI.Endpoints.CourseEndpoints.GetCourseDetails;

public sealed class GetCourseDetailsRequest
{
    public int CourseId { get; set; }
}

public sealed class GetCourseDetailsResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset ModificationDate { get; set; }
    public string? ImageUrl { get; set; }
    public string DifficultyLevel { get; set; } = default!;
    public decimal Price { get; set; } = 0;
    public bool IsFree { get; set; } = true;
    public bool IsPublished { get; set; } = false;
    public string? Prerequisites { get; set; }
    public string? LearningObjectives { get; set; }
    public string? Language { get; set; } = default!;
    public int EstimatedDurationHours { get; set; } = 0;

    public int StudentCount { get; set; } = 0;
    public int RatingCount { get; set; } = 0;
    public double AverageRating { get; set; } = 0.0;

    public InstructorDataResponse? Instructor { get; set; } = default!;

    public bool IsEnrolled { get; set; } = false;
}

public sealed class InstructorDataResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string? ProfilePicture { get; set; }
    public string? Bio { get; set; } = default!;

    public double AverageRating { get; set; } = 0.0;
    public int RatingCount { get; set; } = 0;
    public int CourseCount { get; set; } = 0;
    public int StudentCount { get; set; } = 0;
}