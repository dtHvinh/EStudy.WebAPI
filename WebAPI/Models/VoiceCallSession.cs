using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

[Table("VoiceCallSessions")]
public class VoiceCallSession
{
    public int Id { get; set; }

    public int LearnerId { get; set; }
    public User Learner { get; set; } = default!;

    public int InstructorId { get; set; }
    public User Instructor { get; set; } = default!;

    public DateTimeOffset ScheduledTime { get; set; }

    public int DurationMinutes { get; set; }
    public required string CallUrl { get; set; }
}
