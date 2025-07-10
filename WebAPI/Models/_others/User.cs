using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._course;
using WebAPI.Models._flashCard;

namespace WebAPI.Models._others;

[Table("Users")]
public class User : IdentityUser<int>
{
    public required string Name { get; set; }
    public string? ProfilePicture { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public bool IsOnBoarded { get; set; }
    public string? Bio { get; set; }

    public ICollection<Blog> Blogs { get; set; } = default!;
    public ICollection<FlashCardSet> FlashCardSets { get; set; } = default!;
    public ICollection<CourseEnrollment> Enrollments { get; set; } = default!;
    public ICollection<Course> Courses { get; set; } = default!;
}
