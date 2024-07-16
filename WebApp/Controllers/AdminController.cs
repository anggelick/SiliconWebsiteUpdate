using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebApp.Models.Components;
using WebApp.Models.Views;

namespace WebApp.Controllers;

public class AdminController : Controller
{
    private readonly string _apiKey = "?key=HJFHJEhjeuugbjgor56924ghjf844HJFHJEhjeuugbjgor56924ghjf844";

    private readonly IConfiguration _configuration;

    public AdminController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    #region COURSES

    [HttpGet]
    public async Task<IActionResult> ManageCourses()
    {
        return View();
    }

    #region CREATE
    public async Task<IActionResult> CreateNewCourse(CreateCourseIndexViewModel viewModel)
    {
        viewModel.Title = "Create some new cool course";
        viewModel.Form = new CourseViewModel();

        return View(viewModel);
    }

    public async Task<IActionResult> CreateCourse(CreateCourseIndexViewModel viewModel)
    {
        if(ModelState.IsValid)
        {
            var course = viewModel.Form;
            using var http = new HttpClient();

            var token = HttpContext.Request.Cookies["AccessToken"];
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var courseAsJson = new StringContent(JsonConvert.SerializeObject(course), Encoding.UTF8, "application/json");

            var jsonContent = await courseAsJson.ReadAsStringAsync();
            Console.WriteLine(jsonContent);

            var apiUrl = $"https://localhost:8585/api/courses{_apiKey}";
            var response = await http.PostAsync(apiUrl, courseAsJson);

            if(response.IsSuccessStatusCode)
            {
                viewModel.SuccessMessage = "Great stuff";
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);

                viewModel.ErrorMessage = "Something went wrong, try again!";
            }
        }

        return RedirectToAction("CreateNewCourse", "Admin", viewModel);
    }
    #endregion

    #endregion
}