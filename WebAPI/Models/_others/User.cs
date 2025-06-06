using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._classRoom;
using WebAPI.Models._course;
using WebAPI.Models._flashCard;
using WebAPI.Models._schedule;

namespace WebAPI.Models._others;

[Table("Users")]
public class User : IdentityUser<int>
{
    public required string Name { get; set; }

    public ICollection<Blog> Blogs { get; set; } = default!;
    public ICollection<FlashCardSet> FlashCardSets { get; set; } = default!;
    public ICollection<CourseEnrollment> Enrollments { get; set; } = default!;
    public ICollection<StudySchedule> StudySchedules { get; set; } = default!;
    public ICollection<ClassroomEnrollment> ClassroomEnrollments { get; set; } = default!;
    public ICollection<AssignmentSubmission> AssignmentSubmissions { get; set; } = default!;
}

