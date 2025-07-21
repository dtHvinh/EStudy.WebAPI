using Riok.Mapperly.Abstractions;
using WebAPI.Models._course;

namespace WebAPI.Endpoints.CourseEndpoints.GetCourseToLearn;

[Mapper]
public static partial class Mapper
{
    [MapProperty(nameof(ChapterQuizQuestion.QuestionText), nameof(GetCourseToLearnQuizQuestionResponse.Text))]
    public static partial GetCourseToLearnQuizQuestionResponse ToLearnQuizQuestionResponse(this ChapterQuizQuestion source);

    public static partial List<GetCourseToLearnQuizQuestionResponse> ProjectToListResponse(this IEnumerable<ChapterQuizQuestion?> source);
}
