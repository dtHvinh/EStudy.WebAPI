using FastEndpoints;
using WebAPI.Services;

namespace WebAPI.Endpoints.StorageEndpoints.UploadAudio;

public class Endpoint(FileServiceV2 fileService) : Endpoint<UploadAudioRequest, UploadAudioResponse>
{
    private readonly FileServiceV2 fileService = fileService;

    public override void Configure()
    {
        Post("upload/audio");
        AllowFileUploads();
        Description(x => x
            .WithName("Upload Audio")
            .Produces<string>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
        Group<StorageGroup>();
    }

    public override async Task HandleAsync(UploadAudioRequest req, CancellationToken ct)
    {
        var audioUrl = await fileService.UploadFileAsync(req.File, StorageFileType.Audio, ct);

        await SendOkAsync(new UploadAudioResponse
        {
            AudioUrl = audioUrl
        }, ct);
    }
}