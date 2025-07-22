namespace WebAPI.Endpoints.AdminEndpoints.ContentModerating.HideBlog;

public sealed class ChangeBlogVisibilityRequest
{
    public int BlogId { get; init; }
    public bool IsVisible { get; init; }
}
