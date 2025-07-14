using FastEndpoints;
using WebAPI.Services;

namespace WebAPI.Endpoints.StorageEndpoints.UploadFiles;

public class Endpoint(FileServiceV2 fileService) : Endpoint<UploadFilesRequest, List<string>>
{
    private readonly FileServiceV2 fileService = fileService;
    public override void Configure()
    {
        Post("upload/files");
        AllowFileUploads();
        Description(x => x
            .WithName("Upload Files")
            .Produces<List<string>>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
        Group<StorageGroup>();
    }
    public override async Task HandleAsync(UploadFilesRequest req, CancellationToken ct)
    {
        var fileUrls = await fileService
            .UploadFilesAsync([.. req.Files], StorageFileType.File, ct);

        await SendOkAsync(fileUrls, ct);
    }
}

