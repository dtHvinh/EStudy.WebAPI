using Riok.Mapperly.Abstractions;
using WebAPI.Models._testExam;

namespace WebAPI.Endpoints.TestEndpoints.GetTest;

[Mapper]
public static partial class Mapper
{
    public static partial ICollection<GetTestSection> ProjectToSection(this ICollection<TestSection> sections);

    public static IQueryable<GetTestResponse> ProjectToResponse(this IQueryable<TestExam> queryable)
    {
        return Queryable.Select(queryable, test => new GetTestResponse
        {
            Id = test.Id,
            Title = test.Title,
            Description = test.Description ?? string.Empty,
            Duration = test.Duration,
            PassingScore = test.PassingScore,
            SectionCount = test.Sections.Count,
            AttemptCount = test.AttemptCount,
            QuestionCount = test.Sections.Sum(s => s.Questions.Count),
            Sections = test.Sections.ProjectToSection().ToList(),
        });
    }
}
