using WebApp.Models.Forms;

namespace WebApp.Models.Views;

public class AccountSecurityViewModel
{
    public string? Title { get; set; }
    public string? DeleteAccountMessage { get; set; }
    public string? ChangePasswordSuccessMessage{ get; set; }
    public string? ChangePasswordErrorMessage{ get; set; }
    public PasswordChangeModel? PasswordForm { get; set; }
    public DeleteAccountModel? DeleteAccountForm { get; set; }
    public AccountViewModel? LoggedInUser { get; set; }
}
