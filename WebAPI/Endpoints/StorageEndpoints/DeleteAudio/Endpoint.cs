using FastEndpoints;
using WebAPI.Services;

namespace WebAPI.Endpoints.StorageEndpoints.DeleteAudio;

public class Endpoint(FileServiceV2 fileService) : Endpoint<DeleteAudioRequest>
{
    private readonly FileServiceV2 fileService = fileService;

    public override void Configure()
    {
        Delete("remove/audio/{AudioUrl}");
        Description(x => x
            .WithName("Delete Audio")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
        Group<StorageGroup>();
    }

    public override async Task HandleAsync(DeleteAudioRequest req, CancellationToken ct)
    {
        await fileService.DeleteFileAsync(req.AudioUrl, ct);
        await SendNoContentAsync(ct);
    }
}
