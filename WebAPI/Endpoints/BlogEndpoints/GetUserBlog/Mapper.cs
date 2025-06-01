using Riok.Mapperly.Abstractions;
using WebAPI.Models;

namespace WebAPI.Endpoints.BlogEndpoints.GetUserBlog;

[Mapper]
public static partial class Mapper
{
    public static partial BlogResponse ToBlogResponse(this Blog blog);
}