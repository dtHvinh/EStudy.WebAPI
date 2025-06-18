namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetSetInfo;

public sealed class GetSetInfoRequest
{
    public int SetId { get; set; }
}

public sealed class GetSetInfoResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTimeOffset? LastAccess { get; set; }
    public int Progress { get; set; }
    public int CardCount { get; set; }
}