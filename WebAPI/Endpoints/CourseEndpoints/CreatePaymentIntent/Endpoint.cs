using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models._course;
using WebAPI.Services;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Endpoints.CourseEndpoints.CreatePaymentIntent;

public class Endpoint(
    ApplicationDbContext context,
    PaymentService paymentService) : Endpoint<CreatePaymentIntentRequest, CreatePaymentIntentResponse>
{
    private readonly ApplicationDbContext _context = context;
    private readonly PaymentService _paymentService = paymentService;

    public override void Configure()
    {
        Post("create-payment-intent");
        Description(d => d
            .WithName("Create Payment Intent")
            .WithDescription("Create a payment intent for a course purchase")
            .Produces<CreatePaymentIntentResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status500InternalServerError));
        Group<CourseGroup>();
    }

    public override async Task HandleAsync(CreatePaymentIntentRequest req, CancellationToken ct)
    {
        Course? course = await FindPurchasableCourse(
            _context.Courses.AsQueryable(),
            req.Id,
            int.Parse(this.RetrieveUserId()), ct);
        if (course is null)
        {
            ThrowError("Failed to buy this course", StatusCodes.Status400BadRequest);
            return;
        }

        var clientSecret = await _paymentService.CreateOrGetClientSecret(
            course,
            this.RetrieveUserId(),
            new()
            {
                Metadata = new Dictionary<string, string>
                {
                    { nameof(Course.Title).ToLowerInvariant(), course.Title },
                    { nameof(Course.Description).ToLowerInvariant(), course.Description },
                    { nameof(Course.Price).ToLowerInvariant(), course.Price.ToString() },
                    { "currency", "usd" },
                    { "image", course.ImageUrl ?? string.Empty },
                    { "category", "course" },
                    { "customerId", this.RetrieveUserId() },
                }
            }, ct);

        var response = new CreatePaymentIntentResponse
        {
            ClientSecret = clientSecret,
        };

        await SendOkAsync(response, ct);
    }

    private static async Task<Course?> FindPurchasableCourse(IQueryable<Course> queryable, int courseId, int userId, CancellationToken ct = default)
    {
        return
            await queryable
                    .Where(e =>
                        e.IsPublished &&
                        e.Id == courseId &&
                        !e.Enrollments.Any(er => er.UserId == userId))
                    .FirstOrDefaultAsync(ct);
    }
}
