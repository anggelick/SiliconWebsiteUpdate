using Azure;
using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace ConsoleApp1.Services;

public class MenuService
{

    private readonly AddressRepository _addressRepository;
    private readonly UserProfileRepository _userProfileRepository;
    private readonly ProfilePictureRepository _profilePictureRepository;

    public MenuService(AddressRepository addressRepository, UserProfileRepository userRepository, ProfilePictureRepository profilePictureRepository)
    {
        _addressRepository = addressRepository;
        _userProfileRepository = userRepository;
        _profilePictureRepository = profilePictureRepository;
    }

    public async Task Run()
    {

        //// CREATE AND GET-ALL
        //AddressEntity UserAddress = new();
        //for (int i = 0; i < 10; i++)
        //{
        //    string Id = Guid.NewGuid().ToString();
        //    UserAddress = new();
        //    UserAddress.AddressLine1 = $"{Id}{i}";
        //    UserAddress.AddressLine2 = $"{Id}{i}";
        //    UserAddress.City = $"{Id}{i}"; ;
        //    UserAddress.PostalCode = $"{Id}{i}";

        //    var result = await _addressRepository.CreateAsync(UserAddress);
        //    Console.WriteLine(result.Message);
        //}

        //var addresses = await _addressRepository.GetAllAsync();

        //if (addresses != null)
        //{
        //    foreach (var address in addresses.ContentResult as IEnumerable<AddressEntity>)
        //    {
        //        Console.WriteLine(address.AddressLine1);
        //    }
        //}




        //GET-ONE AND UPDATE











    }
}
