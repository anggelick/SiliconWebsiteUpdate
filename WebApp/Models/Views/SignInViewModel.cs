using WebApp.Models.Forms;

namespace WebApp.Models.Views;

public class SignInViewModel
{
    public string Title { get; set; } = "Sign in";
    public string ErrorMessage { get; set; } = "";
    public SignInModel Form { get; set; } = new();
}
