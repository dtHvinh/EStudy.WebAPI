using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models._report;

[Table("ReportStatuses")]
public class ReportStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
