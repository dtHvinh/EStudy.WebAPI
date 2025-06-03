using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._classRoom;

[Table("SubmissionAttachments")]
public class SubmissionAttachment
{
    public int Id { get; set; }

    public required string FileUrl { get; set; }

    [ForeignKey(nameof(Submission))]
    public int SubmissionId { get; set; }
    public AssignmentSubmission Submission { get; set; } = default!;
}