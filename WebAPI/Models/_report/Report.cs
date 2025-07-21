using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;
using WebAPI.Models.Contract;

namespace WebAPI.Models._report;

[Table("Reports")]
public class Report : IEntityWithTime<int>
{
    public int Id { get; set; }
    public string Type { get; set; } = default!;
    public int TargetId { get; set; }

    public required string Description { get; set; }

    public int ReportReasonId { get; set; } = 0;
    public ReportReason? ReportReason { get; set; }

    public int UserId { get; set; } = 0;
    public User? User { get; set; } = null;

    public DateTimeOffset CreationDate { get; set; }
}
