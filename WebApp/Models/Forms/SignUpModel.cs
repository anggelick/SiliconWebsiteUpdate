using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using WebApp.Helpers;

namespace WebApp.Models.Forms;

public class SignUpModel
{
    [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
    [Required(ErrorMessage = "Please enter first name")]
    public string FirstName { get; set; } = null!;


    [Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
    [Required(ErrorMessage = "Please enter last name")]
    public string LastName { get; set; } = null!;


    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 2)]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please enter a valid email address")]
    [Required(ErrorMessage = "Please enter email address")]
    public string Email { get; set; } = null!;


    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "********", Order = 3)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one numeric character, and one non-alphanumeric character.")]
    [Required(ErrorMessage = "Please enter password")]
    public string Password { get; set; } = null!;


    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password", Prompt = "********", Order = 4)]
    [Compare(nameof(Password), ErrorMessage = "Password does not match")]
    [Required(ErrorMessage = "Please confirm password")]
    public string ConfirmPassword { get; set; } = null!;


    [Display(Name = "I agree to the Terms & Conditions.", Order = 5)]
    [RequiredCheckbox(ErrorMessage = "You must agree to the terms & conditions")]
    public bool TermsAndConditions { get; set; } = false;


    public static implicit operator UserProfileEntity(SignUpModel signUpModel)
    {
        return new UserProfileEntity
        {
            FirstName = signUpModel.FirstName,
            LastName = signUpModel.LastName,
            Email = signUpModel.Email,
        };

    }
}