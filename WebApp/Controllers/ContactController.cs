using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Views;
using Infrastructure.Services;

namespace WebApp.Controllers;

public class ContactController : Controller
{
    private readonly ContactService _contactService;

    public ContactController(ContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var viewModel = new ContactViewModel();
        viewModel.Form = new();

        return View(viewModel);
    }


    [HttpPost]
    public async Task <IActionResult> Index(ContactViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _contactService.SendContactRequest(viewModel.Form);

            if (result.SuccessMessage != null)
            {
                ModelState.Clear();
                viewModel = new ContactViewModel();
            }

            viewModel.ErrorMessage = result.ErrorMessage;
            viewModel.SuccessMessage = result.SuccessMessage;

            return View(viewModel);
        }
        return View(viewModel);
    }
}
