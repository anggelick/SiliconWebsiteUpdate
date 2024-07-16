using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApp.Models.Components;
using WebApp.Models.Forms;
using WebApp.Models.Sections;
using WebApp.Models.Views;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _apiKey = "?key=HJFHJEhjeuugbjgor56924ghjf844HJFHJEhjeuugbjgor56924ghjf844";
        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel
            {
                Title = "Home",

                Showcase = new ShowcaseViewModel
                {
                    Id = "Showcase",
                    ShowcaseImage = new ImageViewModel { ImageUrl = "/images/showcase/showcase-taskmaster.png", AltText = "Showcase image", },
                    Title = "Task Management Assistant You're Gonna Love",
                    Text = "We offer you a new generation of task management system. Plan, manage & track all your tasks in one flexible tool.",

                    BrandsText = "Largest companies use our tool to work efficiently",
                    BrandImages = new List<ImageViewModel>
                    {
                        new() { ImageUrl = "/images/showcase/logoipsum1.svg", AltText = "Logotype for a brand that uses our services." },
                        new() { ImageUrl = "/images/showcase/logoipsum2.svg", AltText = "Logotype for a brand that uses our services." },
                        new() { ImageUrl = "/images/showcase/logoipsum3.svg", AltText = "Logotype for a brand that uses our services." },
                        new() { ImageUrl = "/images/showcase/logoipsum4.svg", AltText = "Logotype for a brand that uses our services." },
                    },

                    Link = new() { ControllerName = "", ActionName = "", Text = "Get started for free" }

                },

                Features = new FeaturesViewModel
                {
                    Id = "Features",
                    Title = "What Do You Get with Our Tool?",
                    Text = "Make sure all your tasks are organized so you can set the priorities and focus on important.",

                    Features = new List<FeatureViewModel>
                    {
                        new() { FeatureImage = new()
                        { ImageUrl = "/images/features/chat.svg", AltText = "" }, FeatureTitle = "Comments on Tasks", FeatureDescription = "Id mollis consectetur congue egestas egestas suspendisse blandit justo." },

                        new() { FeatureImage = new()
                        { ImageUrl = "/images/features/presentation.svg", AltText = "" }, FeatureTitle = "Tasks Analytics", FeatureDescription = "Non imperdiet facilisis nulla tellus Morbi scelerisque eget adipiscing vulputate." },

                        new() { FeatureImage = new()
                        { ImageUrl = "/images/features/add-group.svg", AltText = "" }, FeatureTitle = "Multiple Assignees", FeatureDescription = "A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris." },

                        new() { FeatureImage = new()
                        { ImageUrl = "/images/features/bell.svg", AltText = "" }, FeatureTitle = "Notifications", FeatureDescription = "Diam, suspendisse velit cras ac. Lobortis diam volutpat, eget pellentesque viverra." },

                        new() { FeatureImage = new()
                        { ImageUrl = "/images/features/tasks.svg", AltText = "" }, FeatureTitle = "Sections & Subtasks", FeatureDescription = "Mi feugiat hac id in. Sit elit placerat lacus nibh lorem ridiculus lectus." },

                        new() { FeatureImage = new()
                        { ImageUrl = "/images/features/shield.svg", AltText = "" }, FeatureTitle = "Data Security", FeatureDescription = "Aliquam malesuada neque eget elit nulla vestibulum nunc cras." },
                    }

                },

                Subscriber = new NewsletterModel()
                

            };

            ViewData["Title"] = viewModel.Title;

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Subscribe(NewsletterModel subscriber)
        {
            if (ModelState.IsValid)
            {
                using var http = new HttpClient();

                var subscriberAsJson = new StringContent(JsonConvert.SerializeObject(subscriber), Encoding.UTF8, "application/json");

                var blabla = await subscriberAsJson.ReadAsStringAsync();

                Console.WriteLine(blabla);

                var result = await http.PostAsync($"https://localhost:8585/api/subscribers{_apiKey}", subscriberAsJson);
            }

            return RedirectToAction("Index", "Home")!;
        }
        

        public async Task<IActionResult> UnSubscribe(NewsletterModel subscriber)
        {
            if (ModelState.IsValid)
            {
                using var http = new HttpClient();

                var ApiUrl = $"https://localhost:8585/api/subscribers{_apiKey}?email={Uri.EscapeDataString(subscriber.Email)}";

                var result = await http.DeleteAsync(ApiUrl);
            }

            return RedirectToAction("Index", "Home")!;
        }
    }
}