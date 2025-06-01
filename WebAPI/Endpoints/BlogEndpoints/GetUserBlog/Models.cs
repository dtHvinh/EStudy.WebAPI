using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.BlogEndpoints.GetUserBlog;

public sealed class GetUserBlogRequest : PaginationParams
{
}

public sealed class BlogResponse
{
    public int Id { get; init; }
    public string Title { get; init; } = default!;
    public string Content { get; init; } = default!;
    public DateTimeOffset CreationDate { get; init; }
    public DateTimeOffset ModificationDate { get; init; }
}
