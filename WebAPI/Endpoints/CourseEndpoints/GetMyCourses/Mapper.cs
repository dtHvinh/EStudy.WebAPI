using Riok.Mapperly.Abstractions;
using WebAPI.Models._course;

namespace WebAPI.Endpoints.CourseEndpoints.GetMyCourses;

[Mapper]
public static partial class Mapper
{

    public static IQueryable<GetMyCourseResponse> ProjectToMyCourseResponse(this IQueryable<Course> queryable)
    {
        return Queryable.Select(
            queryable,
            x => new GetMyCourseResponse()
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
                StudentsCount = x.Enrollments.Count
            }
        );
    }
}
