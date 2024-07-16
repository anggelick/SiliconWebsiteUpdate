using Infrastructure.Models;
using WebApp.Models.Components;
using WebApp.Models.Forms;

namespace WebApp.Models.Views;

public class CoursesIndexViewModel
{
    public string Title { get; set; } = "";
    public IEnumerable<CourseViewModel> Courses { get; set; } = null!;
    public IEnumerable<CategoryViewModel> Categories { get; set; } = null!;
    public AccountViewModel Account { get; set; } = null!;
    public Pagination? Pagination { get; set; }
    public string? CourseCategory { get; set; }
    public string? SearchQuery { get; set; }
}
