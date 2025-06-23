using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.BlogEndpoints.SearchBlog;

public sealed class SearchBlogRequest : PaginationParams
{
}

public sealed class SearchBlogResponse
{
    public int Id { get; init; }
    public string Title { get; init; } = default!;
    public DateTimeOffset CreationDate { get; init; }
    public DateTimeOffset? ModificationDate { get; init; }
    public SearchBlogAuthor Author { get; init; } = default!;
}

public sealed class SearchBlogAuthor
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? ProfilePicture { get; set; }
    public DateTimeOffset CreationDate { get; set; }
}