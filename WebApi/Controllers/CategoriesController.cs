using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly CourseCategoryRepository _courseCategoryRepository;

    public CategoriesController(CourseCategoryRepository courseCategoryRepository)
    {
        _courseCategoryRepository = courseCategoryRepository;
    }

    #region READ

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        try
        {
            var result = await _courseCategoryRepository.GetOneAsync(x => x.Id == id);
            if (result != null!)
                return Ok(result);

            return NotFound();
        }
        catch { return Problem(); }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _courseCategoryRepository.GetAllAsync();
            if (result != null!)
                return Ok(result);

            return NotFound();
        }
        catch { return Problem(); }
    }

    #endregion
}
