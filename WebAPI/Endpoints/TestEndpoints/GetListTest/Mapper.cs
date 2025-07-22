using Riok.Mapperly.Abstractions;
using WebAPI.Models._testExam;

namespace WebAPI.Endpoints.TestEndpoints.GetListTest;

[Mapper]
public static partial class Mapper
{
    public static IQueryable<GetListTestResponse> ProjectToGetListTestResponse(this IQueryable<TestExam> query)
    {
        return Queryable.Select(
            query,
            x => new GetListTestResponse()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Duration = x.Duration,
                AttemptCount = x.Attempts.Count,
                QuestionCount = x.Sections.Sum(e => e.Questions.Count),
                SectionCount = x.Sections.Count,
                CommentCount = x.Comments.Count,
                AuthorName = x.Creator.Name,
            }
        );
    }
}
