using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;
using WebAPI.Models.Contract;

namespace WebAPI.Models._course;

[Table("Courses")]
public class Course : IBelongToUser<int>, IEntityWithTime<int>
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }

    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset ModificationDate { get; set; }

    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    public User Author { get; set; } = default!;

    public ICollection<Chapter> Chapters { get; set; } = default!;
    public ICollection<CourseEnrollment> Enrollments { get; set; } = default!;
}
