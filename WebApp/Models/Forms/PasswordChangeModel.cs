using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Forms;

public class PasswordChangeModel
{

    [Display(Name = "Current password", Prompt = "********")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please enter your password")]
    public string CurrentPassword { get; set; } = null!;


    [Display(Name = "New password", Prompt = "********")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one numeric character, and one non-alphanumeric character.")]
    [Required(ErrorMessage = "Please enter new password")] 
    public string NewPassword { get; set; } = null!;


    [Display(Name = "Confirm new password", Prompt = "********")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "Password does not match")]
    [Required(ErrorMessage = "Please confirm new password")]
    public string ConfirmPassword { get; set; } = null!;

}
