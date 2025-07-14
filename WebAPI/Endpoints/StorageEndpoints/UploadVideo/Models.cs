namespace WebAPI.Endpoints.StorageEndpoints.UploadVideo;

public sealed class UploadVideoRequest
{
    public required IFormFile File { get; set; }
}

public sealed class UploadVideoResponse
{
    public required string VideoUrl { get; set; }
}