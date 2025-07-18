using Riok.Mapperly.Abstractions;
using WebAPI.Models._course;

namespace WebAPI.Endpoints.CourseEndpoints.GetCourseDetails;

[Mapper]
public static partial class Mapper
{
    public static IQueryable<GetCourseDetailsResponse> ProjectToCourseDetailsResponse(
        this IQueryable<Course> queryable
    )
    {
        return Queryable.Select(
            queryable,
            x => new GetCourseDetailsResponse()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                CreationDate = x.CreationDate,
                ModificationDate = x.ModificationDate,
                ImageUrl = x.ImageUrl,
                DifficultyLevel = x.DifficultyLevel,
                Price = x.Price,
                IsFree = x.IsFree,
                IsPublished = x.IsPublished,
                Prerequisites = x.Prerequisites,
                LearningObjectives = x.LearningObjectives,
                Language = x.Language,
                EstimatedDurationHours = x.EstimatedDurationHours,

                StudentCount = x.Enrollments.Count,
                AverageRating = x.Ratings.Select(e => e.Value)
                                         .DefaultIfEmpty()
                                         .Average(),
                RatingCount = x.Ratings.Count,
                Instructor = new InstructorDataResponse()
                {
                    Id = x.Author.Id,
                    FullName = x.Author.Name,
                    ProfilePicture = x.Author.ProfilePicture,
                    Bio = x.Author.Bio,
                    AverageRating = x.Author.Courses
                        .SelectMany(c => c.Ratings)
                        .Select(e => e.Value)
                        .DefaultIfEmpty()
                        .Average(),
                    RatingCount = x.Author.Courses.Sum(e => e.Ratings.Count),
                    CourseCount = x.Author.Courses.Count,
                    StudentCount = x.Author.Courses.Select(e => e.Enrollments.Count).DefaultIfEmpty().Sum(),
                }
            }
        );
    }
}
