namespace WebAPI.Endpoints.TestEndpoints.GetTest;

public sealed class GetTestRequest
{
    public int TestId { get; set; }
}

public class GetTestResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Duration { get; set; }
    public int PassingScore { get; set; }
    public int SectionCount { get; set; }
    public int AttemptCount { get; set; }
    public int QuestionCount { get; set; }
    public List<GetTestSection> Sections { get; set; } = default!;
}

public class GetTestSection
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<GetTestQuestion> Questions { get; set; } = default!;
}

public class GetTestQuestion
{
    public int Id { get; set; }
    public string Type { get; set; } = default!;
    public string Text { get; set; } = default!;
    public int Points { get; set; }
    public string? Explanation { get; set; } = default!;
    public List<GetTestAnswer> Answers { get; set; } = default!;
}

public class GetTestAnswer
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;
    public bool IsCorrect { get; set; }
}