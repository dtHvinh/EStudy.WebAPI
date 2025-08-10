using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;
using WebAPI.Models.Contract;

namespace WebAPI.Models._report;

[Table("Reports")]
[Index(nameof(UserId), nameof(Type), nameof(TargetId), IsUnique = true)]
public class Report : IEntityWithTime<int>
{
    public int Id { get; set; }

    public string Type { get; set; } = default!;
    public int TargetId { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User? User { get; set; } = null;

    public required string Description { get; set; }

    [ForeignKey(nameof(ReportReason))]
    public int ReportReasonId { get; set; }
    public ReportReason? ReportReason { get; set; }

    [ForeignKey(nameof(ReportStatus))]
    public int ReportStatusId { get; set; }
    public ReportStatus ReportStatus { get; set; } = default!;

    public DateTimeOffset CreationDate { get; set; }
}
