namespace WebAPI.Endpoints.BlogEndpoints.UpdateBlog;

public sealed class UpdateBlogRequest
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
}
