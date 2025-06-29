using WebAPI.Models._testExam;

namespace WebAPI.Endpoints.TestEndpoints.Related;

public static class Mapper
{
    public static IQueryable<RelatedTestResponse> ProjectToRelatedTestResponse(this IQueryable<TestExam> query)
    {
        return Queryable.Select(
            query,
            x => new RelatedTestResponse()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Duration = x.Duration,
                QuestionCount = x.Sections.Sum(e => e.Questions.Count),
            }
        );
    }
}
