using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models._course;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.EditCourseStructure;

public class Endpoint(ApplicationDbContext context)
    : Endpoint<EditCourseStructureRequest, EditCourseStructureResponse>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Put("{CourseId}/structure");
        Description(x => x
            .WithName("Edit Course Structure")
            .Produces<EditCourseStructureResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithDescription("Edit course structure"));
        Group<CourseGroup>();
    }
    public override async Task HandleAsync(EditCourseStructureRequest request, CancellationToken cancellationToken)
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
            .FirstOrDefaultAsync(e => e.Id == request.CourseId, cancellationToken);

        if (course == null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (course.AuthorId != int.Parse(this.RetrieveUserId()))
        {
            ThrowError("You are not authorized to edit this course.");
            return;
        }

        course.Title = request.Title;
        course.Description = request.Description;
        course.ImageUrl = request.ImageUrl;
        course.DifficultyLevel = request.DifficultyLevel;
        course.Price = request.Price;
        course.IsFree = request.IsFree;
        course.Prerequisites = request.Prerequisites;
        course.LearningObjectives = request.LearningObjectives;
        course.Language = request.Language;
        course.IsPublished = request.IsPublished;
        course.ModificationDate = DateTimeOffset.UtcNow;
        course.Chapters = [.. request.Chapters.Select(chapter => new CourseChapter
        {
            Id = chapter.Id,
            Title = chapter.Title,
            Description = chapter.Description,
            OrderIndex = chapter.OrderIndex,
            IsPublished = chapter.IsPublished,
            Quizzes = [.. chapter.Quizzes.Select(quiz => new ChapterQuiz
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description,
                OrderIndex = quiz.OrderIndex,
                Questions = [.. quiz.Questions.Select(question => new ChapterQuizQuestion
                {
                    Id = question.Id,
                    QuestionText = question.Text,
                    Options = [.. question.Options.Select(option => new ChapterQuizQuestionOption
                    {
                        Id = option.Id,
                        Text = option.Text,
                        IsCorrect = option.IsCorrect
                    })]
                })]
            })],
            Lessons = [.. chapter.Lessons.Select(lesson => new CourseLesson
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Content = lesson.Content,
                Description = lesson.Description,
                DurationMinutes = lesson.DurationMinutes,
                OrderIndex = lesson.OrderIndex,
                TranscriptUrl = lesson.TranscriptUrl,
                VideoUrl = lesson.VideoUrl,
                Attachments = [.. lesson.AttachedFileUrls.Select(url => new LessonAttachment
                {
                    Url = url,
                })]
            })]
        })];

        _context.Courses.Update(course);

        if (await _context.SaveChangesAsync(cancellationToken) > 0)
        {
            await SendOkAsync(new EditCourseStructureResponse
            {
                CourseId = course.Id
            }, cancellationToken);
        }
        else
        {
            ThrowError("Failed to update course structure.");
        }
    }
}
