using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
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
    }
}
