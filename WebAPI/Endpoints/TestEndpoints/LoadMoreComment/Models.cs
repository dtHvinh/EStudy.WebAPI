using WebAPI.Models.Base;
using WebAPI.Utilities.Pagination;

namespace WebAPI.Endpoints.TestEndpoints.LoadMoreComment;

public sealed class LoadMoreCommentRequest : PaginationParams
{
    public int TestId { get; set; }
}

public sealed class LoadMoreCommentResponse : IReadonlySupportResponse
{
    public int Id { get; set; }
    public string Text { get; set; } = default!;
    public LoadMoreCommentAuthor Author { get; set; } = default!;
    public bool IsReadOnly { get; set; }
    public DateTimeOffset CreationDate { get; set; }

    public sealed class LoadMoreCommentAuthor
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string ProfilePicture { get; set; } = default!;
    }
}