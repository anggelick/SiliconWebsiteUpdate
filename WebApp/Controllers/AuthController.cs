using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using WebApp.Models.Forms;
using WebApp.Models.Views;

namespace WebApp.Controllers;

public class AuthController : Controller
{
    private readonly UserProfileRepository _userProfileRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(UserProfileRepository UserProfileRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userProfileRepository = UserProfileRepository;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [Route("/signup")]
    [HttpGet]
    public IActionResult SignUp()
    {
        if (_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("AccountDetails", "Account");
        }
        var viewModel = new SignUpViewModel();
        viewModel.Form = new SignUpModel();
        return View(viewModel);
    }

    [Route("/signup")]
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            ApplicationUser newUser = new ApplicationUser
            {
                FirstName = viewModel.Form.FirstName,
                LastName = viewModel.Form.LastName,
                Email = viewModel.Form.Email,
                UserName = viewModel.Form.Email,

                UserProfile = new UserProfileEntity
                {
                    FirstName = viewModel.Form.FirstName,
                    LastName = viewModel.Form.LastName,
                    Email = viewModel.Form.Email,
                }
            };

            var emailExists = await _userManager.Users.AnyAsync(x => x.Email == newUser.Email);

            if (emailExists)
            {
                viewModel.ErrorMessage = "Email already exists";
                return View(viewModel);
            }
            else
            {
                var result = await _userManager.CreateAsync(newUser, viewModel.Form.Password);
                await _signInManager.PasswordSignInAsync(newUser, viewModel.Form.Password, false, false);
                return RedirectToAction("AccountDetails", "Account");
            }
        }

        viewModel.ErrorMessage = "ERROR";
        return View(viewModel);
    }

    [Route("/signin")]
    [HttpGet]
    public IActionResult SignIn()
    {
        if (_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("AccountDetails", "Account");
        }
        var viewModel = new SignInViewModel();
        viewModel.Form = new SignInModel();
        return View(viewModel);
    }

    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, viewModel.Form.RememberMe, false);
            if (result.Succeeded)
            {
                var user = await _signInManager.UserManager.GetUserAsync(User);
                if (user!.IsAdmin)
                {
                    using var http = new HttpClient();
                    var userAsJson = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    var response = await http.PostAsync($"https://localhost:8585/api/auth", userAsJson);

                    if(response.IsSuccessStatusCode)
                    {
                        var token = await response.Content.ReadAsStringAsync();
                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                        };

                        Response.Cookies.Append("AccessToken", token, cookieOptions);
                    }
                }
                return RedirectToAction("AccountDetails", "Account");
            }
        }
        viewModel.ErrorMessage = "Invalid email or password";
        return View(viewModel);
    }

    public async Task<IActionResult> SignOut()
    {
        Response.Cookies.Delete("AccessToken");
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


    #region Facebook sign in
    [HttpGet]
    public IActionResult Facebook()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallback"));
        return new ChallengeResult("Facebook", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> FacebookCallback()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info != null!)
        {
            var fbUser = new ApplicationUser
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,

                UserProfile = new UserProfileEntity
                {
                    FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                    LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,

                    Address = new AddressEntity
                    {
                        AddressLine1 = info.Principal.FindFirstValue(ClaimTypes.StreetAddress),
                        PostalCode = info.Principal.FindFirstValue(ClaimTypes.PostalCode),
                        City = info.Principal.FindFirstValue(ClaimTypes.Locality),
                    }
                }
            };

            var siliconUser = await _userManager.FindByEmailAsync(fbUser.Email);

            if (siliconUser == null)
            {
                var result = await _userManager.CreateAsync(fbUser);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(fbUser, isPersistent: false);
                    return RedirectToAction("AccountDetails", "Account");
                }
            }

            if (siliconUser != null)
            {
                var userProfile = _userProfileRepository.GetOneAsync(x => x.Email == siliconUser.Email);


                if (fbUser.FirstName != siliconUser.FirstName || fbUser.LastName != siliconUser.LastName || fbUser.Email != siliconUser.Email)
                {
                    siliconUser.FirstName = fbUser.FirstName;
                    siliconUser.LastName = fbUser.LastName;
                    siliconUser.Email = fbUser.Email;


                    siliconUser.UserProfile.FirstName = fbUser.FirstName;
                    siliconUser.UserProfile.LastName = fbUser.LastName;
                    siliconUser.UserProfile.Email = fbUser.Email;

                    await _userManager.UpdateAsync(siliconUser);
                }
                await _signInManager.SignInAsync(siliconUser, isPersistent: false);
                return RedirectToAction("AccountDetails", "Account");

            }
        }
        return View();
    }
    #endregion


    #region Google sign in
    [HttpGet]
    public IActionResult Google()
    {
        var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action("GoogleCallback"));
        return new ChallengeResult("Google", authProps);
    }

    [HttpGet]
    public async Task<IActionResult> GoogleCallback()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();

        string profilePictureUrl = null!;
        foreach (var claim in info!.Principal.Claims)
        {
            if (claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/picture")
            {
                profilePictureUrl = claim.Value;
                break;
            }
        }

        if (info != null!)
        {
            var googleUser = new ApplicationUser
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,

                UserProfile = new UserProfileEntity
                {
                    FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                    LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,

                    Address = new AddressEntity
                    {
                        AddressLine1 = info.Principal.FindFirstValue(ClaimTypes.StreetAddress),
                        PostalCode = info.Principal.FindFirstValue(ClaimTypes.PostalCode),
                        City = info.Principal.FindFirstValue(ClaimTypes.Locality),
                    }
                }
            };

            var siliconUser = await _userManager.FindByEmailAsync(googleUser.Email);

            if (siliconUser == null)
            {
                var result = await _userManager.CreateAsync(googleUser);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(googleUser, isPersistent: false);
                    return RedirectToAction("AccountDetails", "Account");
                }
            }

            if (siliconUser != null)
            {
                var userProfile = await _userProfileRepository.GetOneAsync(x => x.Email == siliconUser.Email);


                if (googleUser.FirstName != siliconUser.FirstName || googleUser.LastName != siliconUser.LastName || googleUser.Email != siliconUser.Email)
                {
                    siliconUser.FirstName = googleUser.FirstName;
                    siliconUser.LastName = googleUser.LastName;
                    siliconUser.Email = googleUser.Email;

                    siliconUser.UserProfile.FirstName = googleUser.FirstName;
                    siliconUser.UserProfile.LastName = googleUser.LastName;
                    siliconUser.UserProfile.Email = googleUser.Email;

                    await _userManager.UpdateAsync(siliconUser);
                }
                await _signInManager.SignInAsync(siliconUser, isPersistent: false);
                return RedirectToAction("AccountDetails", "Account");
            }
        }

        return View();
    }
    #endregion
}