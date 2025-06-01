using Riok.Mapperly.Abstractions;
using WebAPI.Models;

namespace WebAPI.Endpoints.BlogEndpoints.UpdateBlog;

[Mapper(AllowNullPropertyAssignment = false)]
public static partial class Mapper
{
    [MapperIgnoreSource(nameof(Blog.Id))]
    public static partial void UpdateFrom([MappingTarget] this Blog target, UpdateBlogRequest newData);
}