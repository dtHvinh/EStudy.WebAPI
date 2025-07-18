using FastEndpoints;
using Microsoft.AspNetCore.StaticFiles;
using WebAPI.Services;

namespace WebAPI.Endpoints.StorageEndpoints.DonwloadFile;

public class Endpoint(FileServiceV2 fileService) : Endpoint<DownloadFileRequest>
{
    private readonly FileServiceV2 _fileService = fileService;
    public override void Configure()
    {
        Post("download/file");
        AllowAnonymous();
        Description(x => x
            .WithName("Download Files")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
        Group<StorageGroup>();
    }
    public override async Task HandleAsync(DownloadFileRequest req, CancellationToken ct)
    {
        var fileName = req.ObjectName.Split('/').LastOrDefault();
        await _fileService.DownloadFile(req.ObjectName, async stream =>
        {
            await SendStreamAsync(stream, fileName, contentType: "application/octet-stream", enableRangeProcessing: true, cancellation: ct).ConfigureAwait(false);
        }, ct).ConfigureAwait(false);
    }
}

public class EndpointO2(FileServiceV2 fileService) : Endpoint<DownloadFileRequest2>
{
    private readonly FileServiceV2 _fileService = fileService;
    public override void Configure()
    {
        Get("download/file/{FileName}");
        AllowAnonymous();
        Description(x => x
            .WithName("Download Files base on link")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
        Group<StorageGroup>();
    }
    public override async Task HandleAsync(DownloadFileRequest2 req, CancellationToken ct)
    {
        var memoryStream = new MemoryStream();
        var objectName = $"files/{req.FileName}";
        var fileName = objectName.Split('/').Last();
        await _fileService.DownloadFile(objectName, sourceStream =>
        {
            sourceStream.CopyTo(memoryStream);
        }, ct);

        memoryStream.Position = 0;

        var contentType = GetContentType(fileName);

        await SendStreamAsync(memoryStream, contentType: contentType, fileName: fileName, cancellation: ct);
    }

    private static string GetContentType(string fileName)
    {
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(fileName, out var contentType))
            return "application/octet-stream";

        return contentType;
    }
}