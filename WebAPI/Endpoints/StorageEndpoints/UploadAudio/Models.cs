namespace WebAPI.Endpoints.StorageEndpoints.UploadAudio;

public sealed class UploadAudioRequest
{
    public required IFormFile File { get; set; }
}

public sealed class UploadAudioResponse
{
    public required string AudioUrl { get; set; }
}
