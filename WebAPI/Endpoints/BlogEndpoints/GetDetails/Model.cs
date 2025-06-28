using WebAPI.Models.Base;

namespace WebAPI.Endpoints.BlogEndpoints.GetDetails;

public sealed class GetBlogDetailsRequest
{
    public int Id { get; set; }
}

public sealed class GetBlogDetailsResponse : IReadonlySupportResponse
{
    public int Id { get; init; }
    public string Title { get; init; } = default!;
    public string Content { get; init; } = default!;
    public DateTimeOffset CreationDate { get; init; }
    public DateTimeOffset? ModificationDate { get; init; }
    public bool IsReadOnly { get; set; }
}