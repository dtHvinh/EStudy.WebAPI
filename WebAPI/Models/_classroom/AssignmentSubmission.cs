using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;
using WebAPI.Models.Contract;

namespace WebAPI.Models._classRoom;

[Table("AssignmentSubmissions")]
public class AssignmentSubmission : IEntityWithTime<int>
{
    public int Id { get; set; }
    public string? Content { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public bool IsSubmitted { get; set; }
    public double? Grade { get; set; }
    public string? TeacherFeedback { get; set; }
    
    [ForeignKey(nameof(Assignment))]
    public int AssignmentId { get; set; }
    public Assignment Assignment { get; set; } = default!;
    
    [ForeignKey(nameof(Student))]
    public int StudentId { get; set; }
    public User Student { get; set; } = default!;
    
    public ICollection<SubmissionAttachment> Attachments { get; set; } = default!;
}