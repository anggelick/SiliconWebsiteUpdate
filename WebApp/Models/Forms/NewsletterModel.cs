using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Forms;

public class NewsletterModel
{
    [Required]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
    [EmailAddress]
    [Display(Prompt = "Your Email")]
    public string Email { get; set; } = null!;

    [Display(Name = "Daily Newsletter")]
    public bool DailyNewsletter { get; set; } = false;

    [Display(Name = "Advertising Updates")]
    public bool AdvertisingUpdates { get; set; } = false;

    [Display(Name = "Week in Review")]
    public bool WeekInReview { get; set; } = false;

    [Display(Name = "Event Updates")]
    public bool EventUpdates { get; set; } = false;

    [Display(Name = "Startups Weekly")]
    public bool StartupsWeekly { get; set; } = false;

    [Display(Name = "Podcasts")]
    public bool Podcasts { get; set; } = false;
}
