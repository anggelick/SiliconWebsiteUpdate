using Infrastructure.Dtos;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost]
       
        public async Task <IActionResult> GetToken(ApplicationUserDto user)
        {
            if (ModelState.IsValid)
            {
                var userEntity = await _userManager.FindByNameAsync(user.UserName!);
                if (userEntity != null && userEntity.IsAdmin)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new(ClaimTypes.Email, user.Email!),
                            new(ClaimTypes.Name, user.Email!),
                        }),
                        Expires = DateTime.Now.AddDays(2),
                        Issuer = _configuration["JWT:Issuer"],
                        Audience = _configuration["JWT:Audience"],
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    return Ok(tokenString);
                }
            }
            return Unauthorized();
        }
    }
}
