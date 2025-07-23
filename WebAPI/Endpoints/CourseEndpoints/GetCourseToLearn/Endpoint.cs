using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Services;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.GetCourseToLearn;

public class Endpoint(ApplicationDbContext context, CurrentUserService currentUserService) : Endpoint<GetCourseToLearnRequest, GetCourseToLearnResponse>
{
    private readonly ApplicationDbContext _context = context;
    private readonly CurrentUserService _currentUserService = currentUserService;

    public override void Configure()
    {
        Get("{CourseId}/learn");
        Group<CourseGroup>();
    }

    public override async Task HandleAsync(GetCourseToLearnRequest req, CancellationToken ct)
    {
        // TODO: Use split query ?? 
        var isEnrolled = _context.CourseEnrollments
        .Any(e => e.CourseId == req.CourseId && e.UserId == int.Parse(this.RetrieveUserId()));

        if (!await _currentUserService.IsInRoleAsync("Admin"))
        {
            if (!isEnrolled)
            {
                ThrowError("You are not enrolled in this course.", 403);
                return;
            }
        }

        var userId = int.Parse(this.RetrieveUserId());

        var course = await _context.Courses
            .Where(e => e.Id == req.CourseId)
            .Select(e => new GetCourseToLearnResponse
            {
                StudentCount = e.Enrollments.Count(e => e.CourseId == req.CourseId),
                Title = e.Title,
                IsRated = e.Ratings.Any(r => r.UserId == userId),
                AverageRating = e.Ratings.Where(e => e.CourseId == req.CourseId)
                    .Select(e => e.Value)
                    .DefaultIfEmpty()
                    .Average(),
                Chapters = e.Chapters
                    .OrderBy(chapter => chapter.OrderIndex)
                    .Select(chapter => new GetCourseToLearnChapterResponse
                    {
                        Id = chapter.Id,
                        Title = chapter.Title,
                        Description = chapter.Description,
                        OrderIndex = chapter.OrderIndex,
                        IsPublished = chapter.IsPublished,
                        TotalMinutes = chapter.Lessons.Sum(lesson => lesson.DurationMinutes),
                        Lessons = chapter.Lessons
                            .OrderBy(lesson => lesson.OrderIndex)
                            .Select(lesson => new GetCourseToLearnLessonResponse
                            {
                                Id = lesson.Id,
                                Title = lesson.Title,
                                AttachedFileUrls = lesson.Attachments.Select(a => a.Url).ToList(),
                                Content = lesson.Content,
                                Description = lesson.Description,
                                DurationMinutes = lesson.DurationMinutes,
                                OrderIndex = lesson.OrderIndex,
                                TranscriptUrl = lesson.TranscriptUrl,
                                VideoUrl = lesson.VideoUrl,
                                Note = _context.LessonNotes
                                        .Where(n => n.UserId == userId && n.LessonId == lesson.Id)
                                        .Select(n => new GetCourseToLearnNote
                                        {
                                            Content = n.Content
                                        }).FirstOrDefault(),
                                IsCompleted = lesson.LessonProgress.Any(clc => clc.UserId == userId)
                            }).ToList(),
                        Quizzes = chapter.Quizzes
                            .Select(q => new GetCourseToLearnQuizResponse
                            {
                                Id = q.Id,
                                Title = q.Title,
                                Description = q.Description,
                                OrderIndex = q.OrderIndex,
                                IsCompleted = q.QuizProgress.Any(clc => clc.UserId == userId),
                                Questions = q.Questions.Select(qq => new GetCourseToLearnQuizQuestionResponse
                                {
                                    Id = qq.Id,
                                    Text = qq.QuestionText,
                                    Options = qq.Options.Select(o => new GetCourseToLearnQuizQuestionOptionResponse
                                    {
                                        Id = o.Id,
                                        Text = o.Text,
                                        IsCorrect = o.IsCorrect
                                    }).ToList()
                                }).ToList()
                            }).ToList()
                    }).ToList()
            }).FirstOrDefaultAsync(ct);

        if (course == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(course, ct);
    }
}

