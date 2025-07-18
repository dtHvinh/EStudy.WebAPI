using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models._course;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.AddNoteToLesson;

public class Endpoint(ApplicationDbContext context) : Endpoint<AddNoteToLessonRequest>
{
    private readonly ApplicationDbContext _context = context;

    public override void Configure()
    {
        Post("lessons/{LessonId}/note");
        Description(x => x
            .WithName("Add Note to lesson")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Courses"));
        Group<CourseGroup>();
    }
    public override async Task HandleAsync(AddNoteToLessonRequest req, CancellationToken ct)
    {
        var lesson = await _context.Lessons.FindAsync([req.LessonId], ct);
        if (lesson == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        var note = await _context.LessonNotes
            .Where(e => e.LessonId == req.LessonId && e.UserId == int.Parse(this.RetrieveUserId()))
            .FirstOrDefaultAsync(ct);

        if (note != null)
        {
            note.Content = req.Content;
            note.ModificationDate = DateTimeOffset.UtcNow;
            _context.LessonNotes.Update(note);
        }
        else
        {
            note = new UserLessonNote
            {
                Content = req.Content,
                CreationDate = DateTimeOffset.UtcNow,
                LessonId = req.LessonId,
                UserId = int.Parse(this.RetrieveUserId())
            };
            _context.LessonNotes.Add(note);
        }

        await _context.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
