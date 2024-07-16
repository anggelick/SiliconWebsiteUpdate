using WebApp.Models.Components;

namespace WebApp.Models.Views;

public class CreateCourseIndexViewModel
{
    public string Title { get; set; } = "";
    public string? SuccessMessage { get; set; }
    public string? ErrorMessage { get; set; }
    public IEnumerable<CourseViewModel>? Courses { get; set; }
    public CourseViewModel? Form { get; set; }
}
