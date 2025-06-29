namespace WebAPI.Endpoints.TestEndpoints.Related;

public sealed class RelatedTestRequest
{
    public int TestId { get; set; } = default!;
    public int Size { get; set; }
}

public sealed class RelatedTestResponse
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public int QuestionCount { get; set; }
}