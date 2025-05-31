using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models;

[Table("Users")]
public class User : IdentityUser<int>
{
    public required string Name { get; set; }

    public ICollection<Blog> Blogs { get; set; } = default!;
    public ICollection<FlashCardSet> FlashCardSets { get; set; } = default!;
    public ICollection<CourseEnrollment> Enrollments { get; set; } = default!;
    public ICollection<StudySchedule> StudySchedules { get; set; } = default!;
}

