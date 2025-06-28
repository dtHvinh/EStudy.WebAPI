namespace WebAPI.Endpoints.CommentEndpoints.DeleteComment;

public sealed class DeleteCommentRequest
{
    public int CommentId { get; set; }
}

public sealed class DeleteCommentResponse
{
    public int CommentId { get; set; }
}
