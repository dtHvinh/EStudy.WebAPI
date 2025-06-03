using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;

namespace WebAPI.Models._achivement;

[Table("MedalProgress")]
public class MedalProgress
{
    public int Id { get; set; }

    [ForeignKey("Medal")]
    public int MedalId { get; set; }
    public Medal Medal { get; set; } = default!;

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public int Progress { get; set; }

    public DateTimeOffset? AchievementDate { get; set; }
}
