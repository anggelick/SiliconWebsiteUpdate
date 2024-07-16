using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class AddressService
{
    private readonly AddressRepository _addressRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public AddressService(AddressRepository addressRepository, UserManager<ApplicationUser> userManager)
    {
        _addressRepository = addressRepository;
        _userManager = userManager;
    }


    public async Task<bool> CreateAddressAsync (ApplicationUser user)
    {
        var address = user.UserProfile.Address;

        if (address != null)
        {
            var result = await _addressRepository.ExistsAsync(x =>
                x.AddressLine1 == address.AddressLine1 &&
                x.AddressLine2 == address.AddressLine2 &&
                x.PostalCode == address.PostalCode &&
                x.City == address.City);

            switch (result)
            {
                case false:
                    user.UserProfile.Address = address;
                    await _userManager.UpdateAsync(user);
                    break;

                case true:
                    var existingAddress = await _addressRepository.GetOneAsync(x =>
                        x.AddressLine1 == address.AddressLine1 &&
                        x.AddressLine2 == address.AddressLine2 &&
                        x.PostalCode == address.PostalCode &&
                        x.City == address.City);

                    if (existingAddress != null)
                    {
                        user.UserProfile.Address = existingAddress;
                        await _userManager.UpdateAsync(user);
                    }
                    break;
            }
        }

        return false;
    }
}
