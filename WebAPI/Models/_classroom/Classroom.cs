using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;
using WebAPI.Models.Contract;

namespace WebAPI.Models._classRoom;

[Table("Classrooms")]
public class Classroom : IBelongToUser<int>, IEntityWithTime<int>
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? JoinCode { get; set; }
    public DateTimeOffset CreationDate { get; set; }

    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    public User Author { get; set; } = default!;

    public ICollection<ClassroomEnrollment> Enrollments { get; set; } = default!;
    public ICollection<Assignment> Assignments { get; set; } = default!;
}