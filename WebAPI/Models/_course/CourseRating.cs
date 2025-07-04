using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;
using WebAPI.Models.Contract;

namespace WebAPI.Models._course;

[Table("CourseRatings")]
public class CourseRating : IEntityWithTime<int>
{
    public int Id { get; set; }

    [ForeignKey(nameof(Course))]
    public int CourseId { get; set; }
    public Course Course { get; set; } = default!;

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public int Value { get; set; } // 1-5 star rating
    public string? Review { get; set; } // Optional text review
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset? ModificationDate { get; set; } // When rating was last updated

    public bool IsVisible { get; set; } = true; // For moderation purposes
}