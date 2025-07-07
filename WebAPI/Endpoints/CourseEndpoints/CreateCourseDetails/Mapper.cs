using Riok.Mapperly.Abstractions;
using WebAPI.Models._course;

namespace WebAPI.Endpoints.CourseEndpoints.CreateCourseDetails;

[Mapper]
public static partial class Mapper
{
    [MapValue(nameof(Course.CreationDate), Use = nameof(Now))]
    public static partial Course ToCourseDetails(this CreateCourseDetailsRequest request, string authorId);

    private static DateTimeOffset Now() => DateTimeOffset.UtcNow;
}
