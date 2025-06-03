using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.Contract;

namespace WebAPI.Models._classRoom;

[Table("Assignments")]
public class Assignment : IEntityWithTime<int>
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset? DueDate { get; set; }

    [ForeignKey(nameof(Classroom))]
    public int ClassroomId { get; set; }
    public Classroom Classroom { get; set; } = default!;

    public ICollection<AssignmentAttachment> Attachments { get; set; } = default!;
    public ICollection<AssignmentSubmission> Submissions { get; set; } = default!;
}