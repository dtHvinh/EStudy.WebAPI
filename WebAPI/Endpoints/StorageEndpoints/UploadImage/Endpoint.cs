using FastEndpoints;
using WebAPI.Services;

namespace WebAPI.Endpoints.StorageEndpoints.UploadImage;

public class Endpoint(FileServiceV2 fileService) : Endpoint<UploadImageRequest, List<string>>
{
    private readonly FileServiceV2 fileService = fileService;
    public override void Configure()
    {
        Post("upload/images");
        AllowFileUploads();
        Description(x => x
            .WithName("Upload Images")
            .Produces<List<string>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
        Group<StorageGroup>();
    }
    public override async Task HandleAsync(UploadImageRequest req, CancellationToken ct)
    {
        var fileUrls = await fileService
            .UploadFilesAsync([.. req.Files], StorageFileType.Image, ct);

        await SendOkAsync(fileUrls, ct);
    }
}

