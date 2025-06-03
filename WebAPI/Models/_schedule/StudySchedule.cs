using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;
using WebAPI.Models.Contract;

namespace WebAPI.Models._schedule;

[Table("StudySchedules")]
public class StudySchedule : IBelongToUser<int>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public required string ProficiencyGoal { get; set; }
    public int WeeklyTargetMinutes { get; set; }
    public int EstimatedWeek { get; set; }

    public DateTimeOffset? StartDate { get; set; }

    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    public User Author { get; set; } = default!;

    public ICollection<StudyActivity> Activities { get; set; } = default!;
}