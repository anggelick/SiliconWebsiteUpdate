using Newtonsoft.Json;
using WebApp.Models.Components;

namespace WebApp.Services;

public class WebAppCourseService
{

    private readonly IConfiguration _configuration;

    public WebAppCourseService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<CourseResult> GetCoursesAsync(string courseCategory, string searchQuery, int pageNumber = 1, int pageSize = 9)
    {
        using var http = new HttpClient();

        var apiUrl = $"https://localhost:8585/api/courses?" +
            $"courseCategory={courseCategory}&" +
            $"searchQuery={searchQuery}&" +
            $"pageNumber={pageNumber}&" +
            $"pageSize={pageSize}&" +
            $"key={_configuration["ApiKey:Secret"]}";

        var response = await http.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string jsonContentCourses = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CourseResult>(jsonContentCourses);
            if (result != null)
                return result!;
        }
        return null!;
    }

    public async Task<IEnumerable<CategoryViewModel>> GetCourseCategoriesAsync()
    {
        using var http = new HttpClient();

        var apiUrl = $"https://localhost:8585/api/categories";
        var response = await http.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string jsonContentCategories = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(jsonContentCategories);

            return categories!;
        }
        return null!;
    }
}