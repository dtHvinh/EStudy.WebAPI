namespace WebAPI.Endpoints.CourseEndpoints.CreateCourseDetails;

public sealed class CreateCourseDetailsRequest
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public string DifficultyLevel { get; set; } = "Beginner";
    public decimal Price { get; set; } = 0;
    public bool IsFree { get; set; } = true;
    public bool IsPublished { get; set; } = false;
    public string? Prerequisites { get; set; } // Line separated string of prerequisites
    public string? LearningObjectives { get; set; } // Line separated string of learning objectives
    public string? Language { get; set; } = "English";
    public int EstimatedDurationHours { get; set; } = 0;
}

public sealed class CreateCourseDetailsResponse
{
    public int Id { get; set; }
}

