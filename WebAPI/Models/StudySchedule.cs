using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

[Table("StudySchedules")]
public class StudySchedule
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public required string ProficiencyGoal { get; set; }
    public int WeeklyTargetMinutes { get; set; }
    public int EstimatedWeek { get; set; }

    public DateTimeOffset? StartDate { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public ICollection<StudyActivity> Activities { get; set; } = default!;
}