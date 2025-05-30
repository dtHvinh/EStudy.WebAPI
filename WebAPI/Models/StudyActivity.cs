using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

[Table("StudyActivities")]
public class StudyActivity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }

    public int DurationMinutes { get; set; }

    public string? ResourceUrl { get; set; }

    [ForeignKey(nameof(Topic))]
    public int TopicId { get; set; }
    public StudyTopic Topic { get; set; } = default!;

    [ForeignKey(nameof(Schedule))]
    public int ScheduleId { get; set; }
    public StudySchedule Schedule { get; set; } = default!;

    public DateTimeOffset ScheduledTime { get; set; }

    public bool IsCompleted { get; set; }
}