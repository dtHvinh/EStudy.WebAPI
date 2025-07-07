using FastEndpoints;

namespace WebAPI.Endpoints.CourseEndpoints;

public class CourseGroup : Group
{
    public CourseGroup()
    {
        Configure("courses", ep =>
        {
        });
    }
}
