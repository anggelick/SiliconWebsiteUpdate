using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Forms;

public class SignInModel
{
    [Display(Name = "Email", Prompt = "Enter your email address", Order = 0)]
    [Required(ErrorMessage = "Please enter email address")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid email address")] 
    public string Email { get; set; } = null!;


    [DataType(DataType.Password)]
    [Display(Name = "Password", Prompt = "********", Order = 3)]
    [Required(ErrorMessage = "Please enter password")]

    public string Password { get; set; } = null!;

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; } = false;

}
