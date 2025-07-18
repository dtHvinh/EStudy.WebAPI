using Riok.Mapperly.Abstractions;
using WebAPI.Models._course;

namespace WebAPI.Endpoints.CourseEndpoints.GetCourses;

[Mapper]
public static partial class Mapper
{
    public static IQueryable<GetCourseResponse> ProjectToGetCoursesResponse(this IQueryable<Course> courses, int userId)
    {
        return Queryable.Select(
              courses,
              x => new GetCourseResponse()
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
                  StudentsCount = x.Enrollments.Count,
                  IsEnrolled = x.Enrollments.Any(e => e.UserId == userId)
              }
          );
    }
}
