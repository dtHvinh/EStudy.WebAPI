using Minio;
using Minio.DataModel.Args;

namespace WebAPI.Services;

public sealed class BucketArgs
{
    public required string BucketName { get; set; }
}

public enum StorageFileType
{
    Video,
    File,
    Image,
}

public class FileServiceV2(IMinioClient client, BucketArgs bucketArgs)
{
    private readonly IMinioClient _client = client;
    private readonly BucketArgs _bucketArgs = bucketArgs;

    public async Task<string> UploadFileAsync(IFormFile file, StorageFileType fileType, CancellationToken ct = default)
    {
        using var stream = file.OpenReadStream()
            ?? throw new ArgumentNullException(nameof(file), "File stream cannot be null.");

        var parentFolder = fileType.ToString().ToLowerInvariant() + "s";
        var fileName = $"[{Guid.NewGuid().ToString()[..8]}]_{file.FileName.Replace(' ', '_')}";
        var objectName = string.Join(
            separator: '/',
            parentFolder,
            fileName);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_bucketArgs.BucketName)
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(file.Length)
            .WithContentType(file.ContentType);

        await _client.PutObjectAsync(putObjectArgs, ct).ConfigureAwait(false);

        return objectName;
    }

    public async Task DeleteFileAsync(string objectName, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(objectName))
            throw new ArgumentException("File name cannot be null or empty.", nameof(objectName));
        var deleteObjectArgs = new RemoveObjectArgs()
            .WithBucket(_bucketArgs.BucketName)
            .WithObject(objectName);
        await _client.RemoveObjectAsync(deleteObjectArgs, ct).ConfigureAwait(false);
    }

    public async Task<List<string>> UploadFilesAsync(List<IFormFile> files, StorageFileType fileType, CancellationToken ct = default)
    {
        if (files is null || files.Count == 0)
            throw new ArgumentException("Files collection cannot be null or empty.", nameof(files));

        var fileUrls = new List<string>(files.Count);
        foreach (var file in files)
        {
            var fileUrl = await UploadFileAsync(file, fileType, ct).ConfigureAwait(false);
            fileUrls.Add(fileUrl);
        }
        return fileUrls;
    }

    public async Task DeleteFilesAsync(List<string> fileUrls, CancellationToken ct = default)
    {
        if (fileUrls is null || fileUrls.Count == 0)
            throw new ArgumentException("File URLs collection cannot be null or empty.", nameof(fileUrls));
        foreach (var fileUrl in fileUrls)
        {
            await DeleteFileAsync(fileUrl, ct).ConfigureAwait(false);
        }
    }

    public async Task DeleteFilesAsync(string[] fileUrls, CancellationToken ct = default)
    {
        if (fileUrls is null || fileUrls.Length == 0)
            throw new ArgumentException("File URLs collection cannot be null or empty.", nameof(fileUrls));
        foreach (var fileUrl in fileUrls)
        {
            await DeleteFileAsync(fileUrl, ct).ConfigureAwait(false);
        }
    }

    public async Task DownloadFile(string objectName, Action<Stream> callback, CancellationToken ct = default)
    {
        var getObjectArgs = new GetObjectArgs()
            .WithBucket(_bucketArgs.BucketName)
            .WithObject(objectName)
            .WithCallbackStream(callback);

        await _client
            .GetObjectAsync(getObjectArgs, ct)
            .ConfigureAwait(false);
    }
}
