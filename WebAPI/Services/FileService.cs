using Storage;
using Supabase.Storage;
using Supabase.Storage.Interfaces;

namespace WebAPI.Services;

public class FileService(string url, string key) : FileStorageBase(url, key)
{
    private IStorageFileApi<FileObject> EStudyBucket => Client.Storage.From("estudy");
    private readonly Supabase.Storage.FileOptions _options = new() { Upsert = true, CacheControl = "0" };

    public async Task<string> UploadFlashCardImage(IFormFile file, CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken);

        var extension = Path.GetExtension(file.FileName);
        var bucketFilePath = string.Concat("flash-cards/", Guid.NewGuid().ToString().AsSpan(0, 8).ToString(), extension);

        var path = await EStudyBucket.Upload(
            stream.ToArray(),
            bucketFilePath,
            _options);

        return GetPublicUrl(bucketFilePath);
    }

    public async Task UpdateFlashCardImage(string oldPublicUrl, IFormFile file, CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken);

        var extension = Path.GetExtension(file.FileName);
        var pathToUpdate = GetFilePath(oldPublicUrl);
        await EStudyBucket.Update(
            stream.ToArray(),
            pathToUpdate,
            _options);
    }

    public string GetPublicUrl(string path) => EStudyBucket.GetPublicUrl(path);

    public async Task DeleteFlashCardImage(string publicUrl, CancellationToken cancellationToken = default)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            var path = GetFilePath(publicUrl);
            await EStudyBucket.Remove(path);
        }
        catch
        {

        }
    }

    private string GetFilePath(string publicUrl) => publicUrl.Split("public/estudy/")[1];
}
