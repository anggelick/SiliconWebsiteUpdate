using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Forms;

public class AddressViewModel
{
    public int? AddressId { get; set; }


    [Display(Name = "Street name", Prompt = "street name")]
    public string? AddressLine1 { get; set; }


    [Display(Name = "c/o (optional)", Prompt = "c/o")]
    public string? AddressLine2 { get; set; }


    [Display(Name = "Postal code", Prompt = "postal code")]
    public string? PostalCode { get; set; }


    [Display(Name = "City", Prompt = "city")]
    public string? City { get; set; }

    public static implicit operator AddressViewModel(UserProfileEntity userProfile)
    {
        return new AddressViewModel
        {
            AddressLine1 = userProfile.Address?.AddressLine1,
            AddressLine2 = userProfile.Address?.AddressLine2,
            PostalCode = userProfile.Address?.PostalCode,
            City = userProfile.Address?.City,
        };
    }
}