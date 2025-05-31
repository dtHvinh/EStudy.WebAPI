using Riok.Mapperly.Abstractions;
using WebAPI.Models;

namespace WebAPI.Endpoints.BlogEndpoints.Create;

[Mapper]
public static partial class Mapper
{
    /// <summary>
    /// Maps a CreateBlogRequest to a Blog, using the current date as the creation date.
    /// </summary>
    /// <param name="req">The CreateBlogRequest to map.</param>
    /// <param name="authorId">The ID of the author.</param>
    /// <returns>A new Blog instance.</returns>
    [MapValue(nameof(Blog.CreationDate), Use = nameof(GetCurrentDate))]
    public static partial Blog ToBlog(this CreateBlogRequest req, int authorId);

    /// <summary>
    /// Maps a CreateBlogRequest to a Blog, using the current date as the creation date.
    /// </summary>
    /// <param name="req">The CreateBlogRequest to map.</param>
    /// <param name="authorId">The ID of the author as a string.</param>
    /// <returns>A new Blog instance.</returns>
    [MapValue(nameof(Blog.CreationDate), Use = nameof(GetCurrentDate))]
    public static partial Blog ToBlog(this CreateBlogRequest req, string authorId);

    /// <summary>
    /// Maps a CreateBlogRequest to a Blog, using the current date as the creation date.
    /// </summary>
    /// <param name="req">The CreateBlogRequest to map.</param>
    /// <param name="author">The author of the blog.</param>
    /// <returns>A new Blog instance.</returns>
    [MapValue(nameof(Blog.CreationDate), Use = nameof(GetCurrentDate))]
    public static partial Blog ToBlog(this CreateBlogRequest req, User author);

    /// <summary>
    /// Gets the current date and time.
    /// </summary>
    /// <returns>The current date and time.</returns>
    public static DateTimeOffset GetCurrentDate() => DateTimeOffset.UtcNow;
}