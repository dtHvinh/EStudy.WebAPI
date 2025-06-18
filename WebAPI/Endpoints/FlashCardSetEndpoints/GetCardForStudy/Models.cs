using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.FlashCardSetEndpoints.GetCardForStudy;

public sealed class GetCardForStudyRequest : PaginationParams
{
    public int SetId { get; set; }
}

public sealed class FlashCardStudyResponse
{
    public int Id { get; set; }
    public string Term { get; set; } = default!;
    public string Definition { get; set; } = default!;
    public string? Note { get; set; }
    public string? ImageUrl { get; set; }
    public string? Example { get; set; }
    public string? PartOfSpeech { get; set; }
    public bool IsSkipped { get; set; }
}