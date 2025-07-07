using Riok.Mapperly.Abstractions;
using WebAPI.Models._course;

namespace WebAPI.Endpoints.CourseEndpoints.GetCourseDetails;

[Mapper]
public static partial class Mapper
{
    public static partial IQueryable<GetCourseDetailsResponse> ProjectToCourseDetailsResponse(
        this IQueryable<Course> queryable
    );
}
