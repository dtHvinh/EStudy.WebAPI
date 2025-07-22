namespace WebAPI.Endpoints.TestEndpoints.GetCollectionContent;

public sealed class GetCollectionContentRequest
{
    public int CollectionId { get; set; }
}

public sealed class GetCollectionContentResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public List<GetCollectionContentTestResponse> Tests { get; set; } = default!;
}

public sealed class GetCollectionContentTestResponse
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int Duration { get; set; }
    public string AuthorName { get; set; } = default!;

    public int SectionCount { get; set; }
    public int AttemptCount { get; set; }
    public int CommentCount { get; set; }
    public int QuestionCount { get; set; }
}