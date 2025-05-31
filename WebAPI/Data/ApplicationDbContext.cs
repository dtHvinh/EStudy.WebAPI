using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, Role, int>(options)
{
    public DbSet<Answer> Answers { get; set; } = default!;
    public DbSet<Question> Questions { get; set; } = default!;
    public DbSet<Blog> Blogs { get; set; } = default!;
    public DbSet<Chapter> Chapters { get; set; } = default!;
    public DbSet<Course> Courses { get; set; } = default!;
    public DbSet<CourseEnrollment> CourseEnrollments { get; set; } = default!;
    public DbSet<FlashCard> FlashCards { get; set; } = default!;
    public DbSet<FlashCardSet> FlashCardSets { get; set; } = default!;
    public DbSet<Lesson> Lessons { get; set; } = default!;
    public DbSet<TestAnswerSellection> TestAnswerSellections { get; set; } = default!;
    public DbSet<TestAttempt> TestAttempts { get; set; } = default!;
    public DbSet<TestExam> TestExams { get; set; } = default!;
    public DbSet<VoiceCallSession> VoiceCallSessions { get; set; } = default!;
    public DbSet<StudySchedule> StudySchedules { get; set; } = default!;
    public DbSet<StudyActivity> StudyActivities { get; set; } = default!;
    public DbSet<StudyTopic> StudyTopics { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .ToTable("Users")
            .Ignore(e => e.ConcurrencyStamp)
            .Ignore(e => e.LockoutEnabled)
            .Ignore(e => e.LockoutEnd)
            .Ignore(e => e.TwoFactorEnabled)
            .Ignore(e => e.PhoneNumberConfirmed)
            .Ignore(e => e.EmailConfirmed)
            .Ignore(e => e.AccessFailedCount);

        builder.Ignore<IdentityUserLogin<int>>();
        builder.Ignore<IdentityUserToken<int>>();
        builder.Ignore<IdentityUserLogin<int>>();
        builder.Ignore<IdentityRoleClaim<int>>();
        builder.Ignore<IdentityUserClaim<int>>();

        builder.Entity<IdentityUserRole<int>>()
            .ToTable("UserRoles");

        builder.Entity<Role>()
            .ToTable("Roles")
            .HasData([
                new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Id = 2, Name = "Student", NormalizedName = "STUDENT" },
                new Role { Id = 3, Name = "Instructor", NormalizedName = "INSTRUCTOR" }
            ]);
    }
}
