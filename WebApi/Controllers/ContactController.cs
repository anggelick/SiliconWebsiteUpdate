using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class ContactController : ControllerBase
{
    private readonly ContactRequestRepository _contactRequestRepository;

    public ContactController(ContactRequestRepository contactRequestRepository)
    {
        _contactRequestRepository = contactRequestRepository;
    }


    #region CREATE

    [HttpPost]
    public async Task<IActionResult> Create(ContactRequestEntity contactRequest)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var result = await _contactRequestRepository.CreateAsync(contactRequest);
                if (result != null)
                    return Ok(result);
            }
            catch { return Problem(); }
        }
        return BadRequest();
    }

    #endregion

}