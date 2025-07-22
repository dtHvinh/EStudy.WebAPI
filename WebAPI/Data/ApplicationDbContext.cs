using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models._course;
using WebAPI.Models._flashCard;
using WebAPI.Models._others;
using WebAPI.Models._payment;
using WebAPI.Models._report;
using WebAPI.Models._testExam;

namespace WebAPI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User, Role, int>(options)
{
    public DbSet<Resource> Resources { get; set; } = default!;
    public DbSet<Blog> Blogs { get; set; } = default!;

    public DbSet<CourseChapter> Chapters { get; set; } = default!;
    public DbSet<Course> Courses { get; set; } = default!;
    public DbSet<CourseEnrollment> CourseEnrollments { get; set; } = default!;
    public DbSet<CourseLesson> Lessons { get; set; } = default!;
    public DbSet<UserLessonNote> LessonNotes { get; set; } = default!;
    public DbSet<LessonProgress> LessonProgresses { get; set; } = default!;
    public DbSet<CourseRating> CourseRatings { get; set; } = default!;
    public DbSet<ChapterQuiz> ChapterQuizzes { get; set; } = default!;
    public DbSet<ChapterQuizQuestion> ChapterQuizQuestions { get; set; } = default!;
    public DbSet<ChapterQuizQuestionOption> ChapterQuizQuestionOptions { get; set; } = default!;
    public DbSet<QuizProgress> QuizProgresses { get; set; } = default!;


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

    public DbSet<Transaction> Transactions { get; set; } = default!;

    public DbSet<ReportReason> ReportReasons { get; set; } = default!;
    public DbSet<Report> Reports { get; set; } = default!;
    public DbSet<ReportStatus> ReportStatuses { get; set; } = default!;

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

        builder.Entity<ReportStatus>()
            .HasData([
                new ReportStatus { Id = 1, Name = Constants.ReportStatuses.Pending },
                new ReportStatus { Id = 2, Name = Constants.ReportStatuses.UnderReview },
                new ReportStatus { Id = 3, Name = Constants.ReportStatuses.Rejected },
                new ReportStatus { Id = 4, Name = Constants.ReportStatuses.Resolved },
            ]);

        builder.Entity<ReportReason>().HasData(
            new { Id = 1, Title = "Inappropriate Content", Description = "Offensive or adult material." },
            new { Id = 2, Title = "Spam or Scam", Description = "Promotes irrelevant or misleading content." },
            new { Id = 3, Title = "Misinformation", Description = "Incorrect or misleading educational information." },
            new { Id = 4, Title = "Harassment or Abuse", Description = "Bullying or abusive behavior." },
            new { Id = 5, Title = "Copyright Infringement", Description = "Violates intellectual property rights." },
            new { Id = 6, Title = "Violent or Dangerous Content", Description = "Promotes harm or violence." },
            new { Id = 7, Title = "Plagiarism", Description = "Copied from another source without citation." },
            new { Id = 8, Title = "Broken or Unusable Content", Description = "Content cannot be accessed or is broken." },
            new { Id = 9, Title = "Off-topic", Description = "Not relevant to the subject matter." },
            new { Id = 10, Title = "Other", Description = "Other reason not listed." }
        );

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
            .HasMethod("GIN");

        builder.Entity<TestCollection>()
            .HasGeneratedTsVectorColumn(
                p => p.SearchVector,
                "english",  // Text search config
                p => new { p.Name })  // Included properties
            .HasIndex(p => p.SearchVector)
            .HasMethod("GIN"); // Index method on the search vector (GIN or GIST)

        builder.Entity<TestExam>()
            .HasGeneratedTsVectorColumn(
                p => p.SearchVector,
                "english",  // Text search config
                p => new { p.Title })  // Included properties
            .HasIndex(p => p.SearchVector)
            .HasMethod("GIN"); // Index method on the search vector (GIN or GIST)

        builder.Entity<Course>()
            .HasGeneratedTsVectorColumn(
                p => p.SearchVector,
                "english",  // Text search config
                p => new { p.Title })  // Included properties
            .HasIndex(p => p.SearchVector)
            .HasMethod("GIN"); // Index method on the search vector (GIN or GIST)
    }
}
