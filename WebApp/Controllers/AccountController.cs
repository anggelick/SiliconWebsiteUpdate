using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Components;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Models;
using WebApp.Models.Forms;
using Infrastructure.Services;
using Infrastructure.Repositories;
using WebApp.Models.Views;

namespace WebApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AddressService _addressService;
        private readonly UserProfileService _userProfileService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly CourseRepository _courseRepository;
        private readonly SavedCoursesRepository _savedCoursesRepository;
        private readonly CourseService _courseService;
        private readonly IConfiguration _configuration;

        public AccountController(AddressService addressService, UserProfileService userProfileService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CourseRepository courseRepository, SavedCoursesRepository savedCoursesRepository, CourseService courseService, IConfiguration configuration)
        {
            _addressService = addressService;
            _userProfileService = userProfileService;
            _userManager = userManager;
            _signInManager = signInManager;
            _courseRepository = courseRepository;
            _savedCoursesRepository = savedCoursesRepository;
            _courseService = courseService;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("/account-details")]
        public async Task<IActionResult> AccountDetails()
        {
            if (_signInManager.IsSignedIn(User))
            {
                AccountViewModel viewModel = await _userProfileService.GetLoggedInUserAsync(User);

                return View(viewModel);
            }

            return View();
        }
        public async Task<IActionResult> UpdateProfile(AccountViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userProfileService.GetLoggedInUserAsync(User);

                if (user != null)
                {
                    user.FirstName = viewModel.FirstName;
                    user.LastName = viewModel.LastName;
                    user.Email = viewModel.Email;
                    user.UserProfile.FirstName = viewModel.FirstName;
                    user.UserProfile.LastName = viewModel.LastName;
                    user.UserProfile.Email = viewModel.Email;
                    user.UserProfile.PhoneNumber = viewModel.PhoneNumber;
                    user.UserProfile.Bio = viewModel.Bio;

                    await _userManager.UpdateAsync(user);
                }
            }
            return RedirectToAction("AccountDetails", "Account");
        }


        public async Task<IActionResult> UpdateAddress(AccountViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userProfileService.GetLoggedInUserAsync(User);

                if (user != null)
                {
                    user.UserProfile.Address = new AddressEntity
                    {
                        AddressLine1 = viewModel.Address!.AddressLine1,
                        AddressLine2 = viewModel.Address.AddressLine2,
                        PostalCode = viewModel.Address.PostalCode,
                        City = viewModel.Address.City
                    };
                    await _addressService.CreateAddressAsync(user);
                }
            }
            return RedirectToAction("AccountDetails", "Account");
        }

        [Route("/account/SavedItems")]
        public async Task<IActionResult> SavedItems()
        {
            List<CourseViewModel> courses = (await _courseRepository.GetAllAsync())
                .Select(course => (CourseViewModel)course)
                .ToList();
            var userCourses = await _courseService.GetSavedCoursesAsync(User);

            List<CourseViewModel> coursesToDisplay = [];
            foreach (var course in courses)
            {
                var result = userCourses.Any(x => x.CourseId == course.Id);
                if (result)
                {
                    coursesToDisplay.Add(course);
                }
            }

            AccountViewModel viewModel = await _userProfileService.GetLoggedInUserAsync(User);
            viewModel.SavedCourses = coursesToDisplay;

            return View(viewModel);
        }

        public async Task<IActionResult> SaveOrRemoveCourse(CourseViewModel course)
        {
            if (course != null)
            {
                var result = await _courseService.SaveOrRemoveCourseAsync(course.Id, User);
            }
            return RedirectToAction("SavedItems", "Account");
        }

        public async Task<IActionResult> RemoveAllCourses()
        {
            await _courseService.RemoveAllCoursesAssociatedWithUserAsync(User);
            return RedirectToAction("SavedItems", "Account");
        }

        public async Task<IActionResult> SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [Route("/account-security")]    
        public async Task<IActionResult> AccountSecurity(string deleteAccountMessage = "", string changePasswordSuccessMessage = "", string changePasswordErrorMessage = "" )
        {
            if (_signInManager.IsSignedIn(User))
            {
                AccountSecurityViewModel viewModel = new AccountSecurityViewModel();
                viewModel.PasswordForm = new PasswordChangeModel();
                viewModel.LoggedInUser = await _userProfileService.GetLoggedInUserAsync(User);
                viewModel.DeleteAccountMessage = deleteAccountMessage;
                viewModel.ChangePasswordSuccessMessage = changePasswordSuccessMessage;
                viewModel.ChangePasswordErrorMessage = changePasswordErrorMessage;
                return View(viewModel);
            }
            return View();
        }

        public async Task<IActionResult> ChangePassword(AccountSecurityViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userProfileService.GetLoggedInUserAsync(User);

                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user ,viewModel.PasswordForm!.CurrentPassword, viewModel.PasswordForm.NewPassword);
                    if (result.Succeeded)
                        return RedirectToAction("AccountSecurity", "Account", new { changePasswordSuccessMessage = "Password has been changed" });

                }
            }
            return RedirectToAction("AccountSecurity", "Account", new { changePasswordErrorMessage = "Invalid inputs, please try again" });
        }

        public async Task<IActionResult> DeleteAccount(AccountSecurityViewModel viewModel)
        {
            if (ModelState.IsValid) 
            {
                var user = await _userProfileService.GetLoggedInUserAsync(User);

                if (user != null!)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _userManager.DeleteAsync(user);

                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("AccountSecurity", "Account", new { deleteAccountMessage = "You must confirm that you want to delete your account." });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfilePicture(IFormFile file)
        {
            await _userProfileService.ChangeProfilePicture(await _userProfileService.GetLoggedInUserAsync(User), file, _configuration["UploadPath"]!);

            return RedirectToAction("AccountDetails", "Account");
        }
    }
}