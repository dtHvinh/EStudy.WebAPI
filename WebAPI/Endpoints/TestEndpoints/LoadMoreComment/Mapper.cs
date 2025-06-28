using Riok.Mapperly.Abstractions;
using WebAPI.Models._testExam;

namespace WebAPI.Endpoints.TestEndpoints.LoadMoreComment;

[Mapper]
public static partial class Mapper
{
    public static partial IQueryable<LoadMoreCommentResponse> ProjectToResponse(this IQueryable<TestComment> queryable);

    public static IQueryable<LoadMoreCommentResponse> ProjectToResponse(this IQueryable<TestComment> queryable, string userId)
    {
        var comments = queryable.ProjectToResponse().ToList();
        foreach (var comment in comments)
        {
            comment.IsReadOnly = comment.Author.Id != int.Parse(userId);
        }
        return comments.AsQueryable();
    }
}

