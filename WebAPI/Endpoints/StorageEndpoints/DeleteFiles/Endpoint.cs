using FastEndpoints;
using WebAPI.Services;

namespace WebAPI.Endpoints.StorageEndpoints.DeleteFiles;

public class Endpoint(FileServiceV2 fileService) : Endpoint<DeleteFilesRequest>
{
    private readonly FileServiceV2 _fileService = fileService;

    public override void Configure()
    {
        Put("remove/files");
        Description(x => x
            .WithName("Delete Files")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
        Group<StorageGroup>();
    }
    public override async Task HandleAsync(DeleteFilesRequest req, CancellationToken ct)
    {
        await _fileService.DeleteFilesAsync(req.FilePaths, ct);
        await SendNoContentAsync(ct);
    }
}
