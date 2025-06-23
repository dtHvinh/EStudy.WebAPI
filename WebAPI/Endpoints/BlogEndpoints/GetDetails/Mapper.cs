using Riok.Mapperly.Abstractions;
using WebAPI.Models._others;

namespace WebAPI.Endpoints.BlogEndpoints.GetDetails;

[Mapper]
public static partial class Mapper
{
    public static partial GetBlogDetailsResponse ToBlogResponse(this Blog blog);
    public static partial IQueryable<GetBlogDetailsResponse> ProjectToResponse(this IQueryable<Blog> blogs);
}