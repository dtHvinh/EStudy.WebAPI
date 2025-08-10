using Microsoft.AspNetCore.Identity;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models._ai;
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
    public int WarningCount { get; set; }

    public string RefreshToken { get; set; } = string.Empty;

    public NpgsqlTsVector SearchVector { get; set; } = default!;

    public ICollection<Blog> Blogs { get; set; } = default!;
    public ICollection<FlashCardSet> FlashCardSets { get; set; } = default!;
    public ICollection<CourseEnrollment> Enrollments { get; set; } = default!;
    public ICollection<Course> Courses { get; set; } = default!;
    public ICollection<Conversation> AIConversations { get; set; } = default!;
}
