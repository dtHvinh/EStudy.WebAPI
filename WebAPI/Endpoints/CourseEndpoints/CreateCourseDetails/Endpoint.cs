using FastEndpoints;
using WebAPI.Data;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.CreateCourseDetails;

public class Endpoint(ApplicationDbContext context) : Endpoint<CreateCourseDetailsRequest, CreateCourseDetailsResponse>
{
    public override void Configure()
    {
        Post("");
        Group<CourseGroup>();
    }

    public override async Task HandleAsync(CreateCourseDetailsRequest req, CancellationToken ct)
    {
        var newCourse = req.ToCourseDetails(this.RetrieveUserId());

        context.Courses.Add(newCourse);

        if (await context.SaveChangesAsync(ct) > 0)
        {
            await SendOkAsync(new CreateCourseDetailsResponse { Id = newCourse.Id }, ct);
        }
        else
        {
            ThrowError("Failed to create course details. Please try again later.");
        }
    }
}