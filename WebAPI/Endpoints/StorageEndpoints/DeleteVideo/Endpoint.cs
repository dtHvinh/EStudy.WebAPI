using FastEndpoints;
using WebAPI.Services;

namespace WebAPI.Endpoints.StorageEndpoints.DeleteVideo;

public class Endpoint(FileServiceV2 fileService) : Endpoint<DeleteVideoRequest>
{
    private readonly FileServiceV2 fileService = fileService;

    public override void Configure()
    {
        Delete("remove/video/{VideoUrl}");
        Description(x => x
            .WithName("Delete Video")
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
        Group<StorageGroup>();
    }

    public override async Task HandleAsync(DeleteVideoRequest req, CancellationToken ct)
    {
        await fileService.DeleteFileAsync(req.VideoUrl, ct);
        await SendNoContentAsync(ct);
    }
}
