using WebApp.Models.Forms;

namespace WebApp.Models.Views;

public class ContactViewModel
{
    public string Title { get; set; } = "Contact us";
    public ContactModel Form { get; set; } = new ContactModel();
    public string? ErrorMessage { get; set; } 
    public string? SuccessMessage { get; set; } 
}
