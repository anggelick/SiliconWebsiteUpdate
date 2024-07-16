using Infrastructure.Entities;
using Infrastructure.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Infrastructure.Services;

public class ContactService
{
    private readonly string _apiKey = "?key=HJFHJEhjeuugbjgor56924ghjf844HJFHJEhjeuugbjgor56924ghjf844";

    public async Task<ContactResponse> SendContactRequest(ContactRequestEntity contact)
    {
        string success = "Thanks for the message!";
        string badRequest = "Sorry, try again!";
        string error = "Please try again later.";

        ContactResponse response = new();

        using var http = new HttpClient();
        var contactRequestAsJson = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");

        try
        {
            var result = await http.PostAsync($"https://localhost:8585/api/contact{_apiKey}", contactRequestAsJson);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    response.SuccessMessage = success;
                    return response;
                
                case HttpStatusCode.BadRequest:
                    response.ErrorMessage = badRequest;
                    return response;

                case HttpStatusCode.InternalServerError:
                    response.ErrorMessage = error;
                    return response;
            }
        }
        catch
        {
            response.ErrorMessage = error;
            return response;
        }

        response.ErrorMessage = badRequest;
        return response;
    }
}