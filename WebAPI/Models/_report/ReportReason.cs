using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._report;

[Table("ReportReasons")]
public class ReportReason
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
}