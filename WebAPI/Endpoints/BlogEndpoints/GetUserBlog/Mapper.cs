using Riok.Mapperly.Abstractions;
using WebAPI.Models._others;

namespace WebAPI.Endpoints.BlogEndpoints.GetUserBlog;

[Mapper]
public static partial class Mapper
{
    public static partial BlogResponse ToBlogResponse(this Blog blog);

    public static partial IQueryable<BlogResponse> ProjectToResponse(this IQueryable<Blog> blogs);
}