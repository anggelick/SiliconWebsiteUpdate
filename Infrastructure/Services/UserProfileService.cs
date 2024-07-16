using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Infrastructure.Services;

public class UserProfileService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public UserProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser> GetLoggedInUserAsync(ClaimsPrincipal user)
    {
        if (user.Identity!.IsAuthenticated)
        {
            var loggedInUser = await _userManager.Users
                .Include(x => x.UserProfile)
                .Include(x => x.UserProfile.Address)
                .Include(x => x.UserProfile.ProfilePicture)
                .Include(x => x.UserProfile.SavedItems)
                .FirstOrDefaultAsync(x => x.Email == user.Identity.Name);

            if ( loggedInUser != null! )
            {
                return loggedInUser;
            }
        }

        return null!;
    }

    public async Task<bool> ChangeProfilePicture(ApplicationUser user, IFormFile file, string uploadPath)
    {
        var fileName = $"{user.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, fileName);

        using var fs = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fs);

        user.UserProfile.ProfilePicture = new ProfilePictureEntity
        {
            ImageUrl = fileName
        };

        await _userManager.UpdateAsync(user);

        return true;
    }
}
