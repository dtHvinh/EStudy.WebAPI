using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Endpoints.CourseEndpoints.EditCourseStructure;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.GetCourseStructure;

public class Endpoint(ApplicationDbContext context)
    : Endpoint<GetCourseStructureRequest, GetCourseStructureResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Get("{CourseId}/structure");
        Description(x => x
            .WithName("Get Course Structure")
            .Produces<EditCourseStructureResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithDescription("Get course structure"));
        Group<CourseGroup>();
    }
    public override async Task HandleAsync(GetCourseStructureRequest request, CancellationToken ct)
    {
        var course = await _context.Courses
            .Include(e => e.Author)
            .Include(e => e.Chapters)
            .ThenInclude(e => e.Lessons)
            .ThenInclude(e => e.Attachments)
            .Include(e => e.Chapters)
            .ThenInclude(e => e.Quizzes)
            .ThenInclude(e => e.Questions)
            .ThenInclude(e => e.Options)
            .AsSplitQuery()
            .FirstOrDefaultAsync(e => e.Id == request.CourseId, ct);

        if (course is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        if (course.AuthorId != int.Parse(this.RetrieveUserId()))
        {
            ThrowError("You are not authorized to get this course structure.");
            return;
        }

        var response = new GetCourseStructureResponse()
        {
            CourseId = course.Id,
            IsPublished = course.IsPublished,
            Chapters = [.. course.Chapters.Select(chapter => new GetCourseStructureChapterResponse
            {
                Id = chapter.Id,
                Title = chapter.Title,
                Description = chapter.Description,
                OrderIndex = chapter.OrderIndex,
                IsPublished = chapter.IsPublished,
                Quizzes = [.. chapter.Quizzes.Select(quiz => new GetCourseStructureQuizResponse
                {
                    Id = quiz.Id,
                    Title = quiz.Title,
                    Description = quiz.Description,
                    OrderIndex = quiz.OrderIndex,
                    Questions = [.. quiz.Questions.Select(question => new GetCourseStructureQuizQuestionResponse
                    {
                        Id = question.Id,
                        Text = question.QuestionText,
                        Options = [.. question.Options.Select(option => new GetCourseStructureQuizQuestionOptionResponse
                        {
                            Id = option.Id,
                            Text = option.Text,
                            IsCorrect = option.IsCorrect
                        })]
                    })]
                })],
                Lessons = [.. chapter.Lessons.Select(lesson => new GetCourseStructureLessonResponse
                {
                    Id = lesson.Id,
                    Title = lesson.Title,
                    AttachedFileUrls = [..lesson.Attachments.Select(e => e.Url)],
                    Content = lesson.Content,
                    Description = lesson.Description,
                    DurationMinutes = lesson.DurationMinutes,
                    OrderIndex = lesson.OrderIndex,
                    TranscriptUrl = lesson.TranscriptUrl,
                    VideoUrl = lesson.VideoUrl
                })],
            })]
        };

        await SendOkAsync(response, ct);
    }
}
