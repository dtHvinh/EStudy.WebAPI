using Riok.Mapperly.Abstractions;
using WebAPI.Models._others;

namespace WebAPI.Endpoints.BlogEndpoints.SearchBlog;

[Mapper]
public static partial class Mapper
{
    public static partial IQueryable<SearchBlogResponse> ProjectToSearchResponse(this IQueryable<Blog> blogs);
}
