using Minio;
using Minio.DataModel.Args;

namespace WebAPI.Services;

public sealed class BucketArgs
{
    public required string BucketName { get; set; }
}

public enum StorageFileType
{
    Video
}

public class FileServiceV2(IMinioClient client, BucketArgs bucketArgs)
{
    private readonly IMinioClient _client = client;
    private readonly BucketArgs _bucketArgs = bucketArgs;

    public async Task<string> UploadFileAsync(IFormFile file, StorageFileType fileType, CancellationToken ct)
    {
        using var stream = file.OpenReadStream()
            ?? throw new ArgumentNullException(nameof(file), "File stream cannot be null.");

        var objectName = string.Join(
            fileType.ToString().ToLowerInvariant(),
            $"[{Guid.NewGuid().ToString()[..5]}]_",
            file.FileName);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_bucketArgs.BucketName)
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(file.Length)
            .WithContentType(file.ContentType);

        await _client.PutObjectAsync(putObjectArgs, ct).ConfigureAwait(false);

        return objectName;
    }
}
