using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._classRoom;

[Table("AssignmentAttachments")]
public class AssignmentAttachment
{
    public int Id { get; set; }
    public required string FileName { get; set; }
    public required string FileUrl { get; set; }
    public string? FileType { get; set; }
    public long? FileSize { get; set; }
    
    [ForeignKey(nameof(Assignment))]
    public int AssignmentId { get; set; }
    public Assignment Assignment { get; set; } = default!;
}