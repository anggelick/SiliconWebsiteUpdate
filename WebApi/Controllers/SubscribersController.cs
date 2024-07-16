using Infrastructure.Dtos;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class SubscribersController : ControllerBase
{
    private readonly SubscriberRepository _subscriberRepository;

    public SubscribersController(SubscriberRepository subscriberRepository)
    {
        _subscriberRepository = subscriberRepository;
    }


    #region CREATE

    [HttpPost]
    public async Task<IActionResult> Create(SubscriberDto subscriber)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var exists = await _subscriberRepository.ExistsAsync(x => x.Email == subscriber.Email);

                if (exists)
                {
                    return Conflict();
                }

                var result = await _subscriberRepository.CreateAsync(subscriber);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex) { return Problem(); }
        }
        return BadRequest();
    }

    #endregion


    #region DELETE 

    [HttpDelete]
    public async Task<IActionResult> Delete(SubscriberDto subscriber)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var exists = await _subscriberRepository.ExistsAsync(x => x.Email == subscriber.Email);

                if (exists)
                {
                    var result = _subscriberRepository.DeleteAsync(x => x.Email == subscriber.Email);
                    if (result.Result != null)
                    {
                        return Ok();
                    }
                    else 
                        return BadRequest();
                }
                return NotFound();
            }
            catch (Exception ex) { return Problem(); }
        }
        return BadRequest();
    }

    #endregion

}
