namespace WebApp.Models.Components;

public class CourseResult
{
    public List<CourseViewModel> Courses { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}
