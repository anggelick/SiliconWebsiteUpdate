using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models.Components;
using WebApp.Models.Views;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly UserProfileService _userProfileService;
        private readonly CourseService _courseService;
        private readonly IConfiguration _configuration;
        private readonly WebAppCourseService _webAppCourseService;

        public CoursesController(UserProfileService userProfileService, CourseService courseService, IConfiguration configuration, WebAppCourseService webAppCourseService)
        {
            _userProfileService = userProfileService;
            _courseService = courseService;
            _configuration = configuration;
            _webAppCourseService = webAppCourseService;
        }

        
        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> Index(string courseCategory = "", string searchQuery = "", int pageNumber = 1, int pageSize = 9)
        {
            await _courseService.RunAsync();

            using var http = new HttpClient();

            var courseResult = await _webAppCourseService.GetCoursesAsync(courseCategory, searchQuery, pageNumber, pageSize);
            var categories = await _webAppCourseService.GetCourseCategoriesAsync();


            var userCourses = await _courseService.GetSavedCoursesAsync(User);
            var user = await _userProfileService.GetLoggedInUserAsync(User);

            if (courseResult != null)
            {
                foreach (var course in courseResult.Courses)
                {
                    var result = user.UserProfile.SavedItems?.Any(x => x.CourseId == course.Id);
                    if (result == true)
                    {
                        course.IsSaved = true;
                    }
                }
            }

            var viewModel = new CoursesIndexViewModel
            {
                Courses = courseResult!.Courses,
                Categories = categories!,
                Title = "Courses",
                Pagination = new Pagination
                {
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = courseResult.TotalPages,
                    TotalItems = courseResult.TotalItems,
                }
            };

            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            using var http = new HttpClient();
            CourseViewModel viewModel = new();

            var result = await http.GetAsync($"https://localhost:8585/api/courses/{id}/?key={_configuration["ApiKey:Secret"]}");

            if (result.IsSuccessStatusCode)
            {
                string jsonContent = await result.Content.ReadAsStringAsync();
                viewModel = JsonConvert.DeserializeObject<CourseViewModel>(jsonContent)!;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> SaveCourse(CourseViewModel course, CoursesIndexViewModel viewModel)
        {
            if (course != null)
            {
                var result = await _courseService.SaveOrRemoveCourseAsync(course.Id, User);
            }
            return RedirectToAction("index", viewModel);
        }
    }
}