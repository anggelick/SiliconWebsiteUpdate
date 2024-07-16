using Infrastructure.Entities;

namespace Infrastructure.Models;

public class CourseResponse
{
    public List<CourseEntity> Courses { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}
