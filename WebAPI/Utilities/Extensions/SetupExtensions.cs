using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Minio;
using Quartz;
using Quartz.AspNetCore;
using StackExchange.Redis;
using Stripe;
using System.Text;
using WebAPI.BackgroundJobs;
using WebAPI.Data;
using WebAPI.Middlewares.Contract;
using WebAPI.Models._others;
using WebAPI.Services;
using IOE = System.InvalidOperationException;
namespace WebAPI.Utilities.Extensions;

public static class SetupExtensions
{
    public static IConfiguration Config { get; set; } = default!;

    public static IServiceCollection AddClients(this IServiceCollection services)
    {
        services.AddHttpClient("GoogleClient", client => client.BaseAddress = new Uri("https://www.googleapis.com"));

        return services;
    }

    public static IServiceCollection ConfigureDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(Config.GetConnectionString("Postgre"));

            //options.EnableSensitiveDataLogging();
        });

        services.AddIdentity<User, Models._others.Role>(options =>
        {
            options.User.RequireUniqueEmail = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }

    public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services)
    {
        var jwtSettings = Config.GetSection("JWT");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret must be configured"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
         {
             options.AddPolicy(name: "Default",
                               policy =>
                               {
                                   policy.WithOrigins(
                                       "http://localhost:7285",
                                       "http://localhost:3000",
                                       "https://stunning-full-heron.ngrok-free.app",
                                       "https://e-study-client.vercel.app"
                                       );
                                   policy.AllowAnyHeader();
                                   policy.AllowAnyMethod();
                                   policy.AllowCredentials();
                               });
         });

        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        // Register services using the attribute-based approach
        services.AddServicesFromAssembly(typeof(SetupExtensions).Assembly);

        services.AddSingleton(cf =>
        {
            var supabaseKey = Config["Supabase:Key"]
            ?? throw new IOE("Supabase Key must be configured");
            var supabaseUrl = Config["Supabase:Url"]
            ?? throw new IOE("Supabase Url must be configured");

            var storage = new Services.FileService(supabaseUrl, supabaseKey);
            storage.InitializeAsync();
            return storage;
        });

        services.AddMinio(cf => cf
                .WithEndpoint(Config["Minio:Url"])
                .WithCredentials(Config["Minio:AccessKey"], Config["Minio:SecretKey"])
                .WithSSL(false)
                .Build());

        services.AddSingleton(cf =>
        {
            var client = cf.GetRequiredService<IMinioClient>();
            var bucketName = Config["Minio:BucketName"]
            ?? throw new IOE("Minio bucket name must be configured");

            var bucketArgs = new BucketArgs { BucketName = bucketName };

            return new FileServiceV2(client, bucketArgs);
        });

        return services;
    }

    public static IServiceCollection RegisterPayment(this IServiceCollection services)
    {
        StripeConfiguration.ApiKey = Config["Stripe:ApiKey"];

        return services;
    }

    public static IServiceCollection AddCaching(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Config.GetConnectionString("Redis")!));
        return services;
    }

    public static IServiceCollection AddJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseInMemoryStore();

            q.ScheduleJob<TestJob>(trigger => trigger
                .WithIdentity("Combined Configuration Trigger")
                .StartAt(TestJob.StartAt)
                .WithCalendarIntervalSchedule(TestJob.GetJobScheduler())
                .WithDescription("My awesome trigger configured for a job with single call"));
        });

        services.AddQuartzServer(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        return services;
    }

    public static IApplicationBuilder UseApplicationMiddlewares(this IApplicationBuilder builder)
    {
        var middlewares = typeof(SetupExtensions).Assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IApplicationMiddleware).IsAssignableFrom(t))
            .ToList();

        foreach (var middleware in middlewares)
        {
            builder.UseMiddleware(middleware);
        }

        return builder;
    }
}
