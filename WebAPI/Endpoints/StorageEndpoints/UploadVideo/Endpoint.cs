using FastEndpoints;
using WebAPI.Services;

namespace WebAPI.Endpoints.StorageEndpoints.UploadVideo;

public class Endpoint(FileServiceV2 fileService) : Endpoint<UploadVideoRequest, UploadVideoResponse>
{
    private readonly FileServiceV2 fileService = fileService;

    public override void Configure()
    {
        Post("upload/video");
        AllowFileUploads();
        Description(x => x
            .WithName("Upload Video")
            .Produces<string>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
        Group<StorageGroup>();
    }

    public override async Task HandleAsync(UploadVideoRequest req, CancellationToken ct)
    {
        var videoUrl = await fileService.UploadFileAsync(req.File, StorageFileType.Video, ct);

        await SendOkAsync(new UploadVideoResponse
        {
            VideoUrl = videoUrl
        }, ct);
    }
}
