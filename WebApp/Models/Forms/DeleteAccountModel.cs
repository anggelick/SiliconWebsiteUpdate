using System.ComponentModel.DataAnnotations;
using WebApp.Helpers;

namespace WebApp.Models.Forms;

public class DeleteAccountModel
{
    [Display(Name = "Yes, I want to delete my account.", Order = 5)]
    [RequiredCheckbox(ErrorMessage = "You must check this box")]
    public bool TermsAndConditions { get; set; } = false;
}
