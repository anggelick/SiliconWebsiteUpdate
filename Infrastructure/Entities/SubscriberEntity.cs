using Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class SubscriberEntity
{
    public int Id { get; set; }

    [Required]
    [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
    [EmailAddress] 
    public string Email { get; set; } = null!;
    public bool DailyNewsletter { get; set; } = false;
    public bool AdvertisingUpdates { get; set; } = false;
    public bool WeekInReview { get; set; } = false;
    public bool EventUpdates { get; set; } = false;
    public bool StartupsWeekly { get; set; } = false;
    public bool Podcasts { get; set; } = false;

    public static implicit operator SubscriberEntity(SubscriberDto dto)
    {
        return new SubscriberEntity
        {
            Email = dto.Email,
            DailyNewsletter = dto.DailyNewsletter,
            AdvertisingUpdates = dto.AdvertisingUpdates,
            WeekInReview = dto.WeekInReview,
            EventUpdates = dto.EventUpdates,
            StartupsWeekly = dto.StartupsWeekly,
            Podcasts = dto.Podcasts
        };
    }
}