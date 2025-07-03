using Riok.Mapperly.Abstractions;
using WebAPI.Models._testExam;
using static WebAPI.Endpoints.TestEndpoints.GetDetails.GetTestDetailsResponse;

namespace WebAPI.Endpoints.TestEndpoints.GetDetails;

[Mapper]
public static partial class Mapper
{
    public static partial List<GetTestDetailsSection> ProjectToSectionResponse(this ICollection<TestSection> sections);
    private static partial List<GetTestDetailsComment> ProjectToCommentResponse(this IEnumerable<TestComment> comments);

    public static List<GetTestDetailsComment> ProjectToCommentResponse(this ICollection<TestComment> comments, string authorId, int commentSkip, int commentTake)
    {
        var res = ProjectToCommentResponse(
            comments: comments
                    .OrderByDescending(e => e.CreationDate)
                    .Skip(commentSkip)
                    .Take(commentTake));
        foreach (var comment in res)
        {
            comment.IsReadOnly = comment.Author.Id != int.Parse(authorId);
        }

        return res;
    }

    public static IQueryable<GetTestDetailsResponse> ProjectToGetDetailsResponse(this IQueryable<TestExam> query, string authorId, int commentSkip, int commentTake)
    {
        return Queryable.Select(
            query,
            x => new GetTestDetailsResponse()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Duration = x.Duration,
                AttemptCount = x.Attempts.Count,
                QuestionCount = x.Sections.Sum(e => e.Questions.Count),
                SectionCount = x.Sections.Count,
                CommentCount = x.Comments.Count,

                Sections = ProjectToSectionResponse(x.Sections),
                Comments = ProjectToCommentResponse(x.Comments, authorId, commentSkip, commentTake),
            }
        );
    }
}
