using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._others;

namespace WebAPI.Models._classRoom;

[Table("ClassroomEnrollments")]
public class ClassroomEnrollment
{
    public int Id { get; set; }

    [ForeignKey(nameof(Classroom))]
    public int ClassroomId { get; set; }
    public Classroom Classroom { get; set; } = default!;

    [ForeignKey(nameof(Student))]
    public int StudentId { get; set; }
    public User Student { get; set; } = default!;

    public DateTimeOffset JoinDate { get; set; }
}