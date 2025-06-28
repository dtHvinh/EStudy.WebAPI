using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models._course;
using WebAPI.Models._flashCard;
using WebAPI.Models._others;
using WebAPI.Models._testExam;

namespace WebAPI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User, Role, int>(options)
{
    public DbSet<Resource> Resources { get; set; } = default!;
    public DbSet<Blog> Blogs { get; set; } = default!;

    public DbSet<Chapter> Chapters { get; set; } = default!;
    public DbSet<Course> Courses { get; set; } = default!;
    public DbSet<CourseEnrollment> CourseEnrollments { get; set; } = default!;
    public DbSet<Lesson> Lessons { get; set; } = default!;

    public DbSet<FlashCard> FlashCards { get; set; } = default!;
    public DbSet<FlashCardSet> FlashCardSets { get; set; } = default!;

    public DbSet<Answer> Answers { get; set; } = default!;
    public DbSet<Question> Questions { get; set; } = default!;
    public DbSet<TestAnswerSellection> TestAnswerSellections { get; set; } = default!;
    public DbSet<TestAttempt> TestAttempts { get; set; } = default!;
    public DbSet<TestCollection> TestCollections { get; set; } = default!;
    public DbSet<TestExam> TestExams { get; set; } = default!;
    public DbSet<TestSection> TestSections { get; set; } = default!;
    public DbSet<TestComment> TestComments { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder
            .Ignore<IdentityUserLogin<int>>()
            .Ignore<IdentityUserToken<int>>()
            .Ignore<IdentityUserLogin<int>>()
            .Ignore<IdentityRoleClaim<int>>()
            .Ignore<IdentityUserClaim<int>>();

        builder.Entity<User>()
            .ToTable("Users")
            .Ignore(e => e.ConcurrencyStamp)
            .Ignore(e => e.LockoutEnabled)
            .Ignore(e => e.LockoutEnd)
            .Ignore(e => e.TwoFactorEnabled)
            .Ignore(e => e.PhoneNumberConfirmed)
            .Ignore(e => e.EmailConfirmed)
            .Ignore(e => e.AccessFailedCount);

        builder.Entity<IdentityUserRole<int>>()
            .ToTable("UserRoles");

        builder.Entity<Role>()
            .ToTable("Roles")
            .HasData([
                new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Id = 2, Name = "Student", NormalizedName = "STUDENT" },
                new Role { Id = 3, Name = "Instructor", NormalizedName = "INSTRUCTOR" }
            ]);

        builder.Entity<FlashCard>()
            .HasGeneratedTsVectorColumn(
                p => p.SearchVector,
                "english",  // Text search config
                p => new { p.Term, p.Definition, p.PartOfSpeech })  // Included properties
            .HasIndex(p => p.SearchVector)
            .HasMethod("GIN"); // Index method on the search vector (GIN or GIST)

        builder.Entity<Blog>()
            .HasGeneratedTsVectorColumn(
                p => p.SearchVector,
                "english",  // Text search config
                p => new { p.Title })  // Included properties
            .HasIndex(p => p.SearchVector)
            .HasMethod("GIN"); // Index method on the search vector (GIN or GIST)

        builder.Entity<TestExam>()
            .HasGeneratedTsVectorColumn(
                p => p.SearchVector,
                "english",  // Text search config
                p => new { p.Title })  // Included properties
            .HasIndex(p => p.SearchVector)
            .HasMethod("GIN"); // Index method on the search vector (GIN or GIST)
    }
}
