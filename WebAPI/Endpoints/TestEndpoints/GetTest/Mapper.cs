using Riok.Mapperly.Abstractions;
using WebAPI.Models._testExam;

namespace WebAPI.Endpoints.TestEndpoints.GetTest;

[Mapper]
public static partial class Mapper
{
    public static IQueryable<GetTestResponse> ProjectToGetTestResponse(this IQueryable<TestExam> query)
    {
        return Queryable.Select(
            query,
            x => new GetTestResponse()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Duration = x.Duration,
                AttemptCount = x.AttemptCount,
                QuestionCount = x.Sections.Sum(e => e.Questions.Count),
                SectionCount = x.Sections.Count,
                CommentCount = x.Comments.Count
            }
        );
    }
}
