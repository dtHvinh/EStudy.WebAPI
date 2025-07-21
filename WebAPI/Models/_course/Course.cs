using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;
using WebAPI.Models.Contract;

namespace WebAPI.Models._course;

[Table("Courses")]
public class Course : IBelongToUser<int>, IEntityWithTime<int>, IPurchasable<int>
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset ModificationDate { get; set; }
    public string? ImageUrl { get; set; }
    public string DifficultyLevel { get; set; } = "Beginner";
    public decimal Price { get; set; } = 0;
    public bool IsFree { get; set; } = true;
    public bool IsPublished { get; set; } = false;
    public string? Prerequisites { get; set; } // Line separated string of prerequisites
    public string? LearningObjectives { get; set; } // Line separated string of learning objectives
    public string? Language { get; set; } = "English";
    public int EstimatedDurationHours { get; set; } = 0;

    public NpgsqlTsVector SearchVector { get; set; } = default!;

    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    public User Author { get; set; } = default!;

    public ICollection<CourseChapter> Chapters { get; set; } = default!;
    public ICollection<CourseEnrollment> Enrollments { get; set; } = default!;
    public ICollection<CourseRating> Ratings { get; set; } = default!;
}