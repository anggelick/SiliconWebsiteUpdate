using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Filters;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class CoursesController : ControllerBase
{
    private readonly CourseRepository _courseRepository;
    private readonly CourseService _courseService;

    public CoursesController(CourseRepository courseRepository, CourseService courseService)
    {
        _courseRepository = courseRepository;
        _courseService = courseService;
    }


    #region CREATE

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task <IActionResult> Create(CourseEntity course)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var result = await _courseService.CreateCourseAsync(course);
                if (result != null)
                    return Ok();
            }
            catch { return Problem(); }
        }
        return BadRequest();
    }

    #endregion


    #region READ

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        try
        {
            var result = await _courseRepository.GetOneAsync(x => x.Id == id);
            if (result != null!)
                return Ok(result);

            return NotFound();
        }
        catch { return Problem(); }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string courseCategory = "", string searchQuery = "", int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            #region Query Filters
            var courses = _courseRepository.GetAll();

            var query = courses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(courseCategory))
                query = query.Where(x => x.CourseCategory!.Name.Contains(courseCategory));

            if (!string.IsNullOrWhiteSpace(searchQuery))
                query = query.Where(x => x.Name.Contains(searchQuery) || x.CourseAuthor!.Name.Contains(searchQuery));

            #endregion

            var response = new CourseResponse
            {
                TotalItems = await query.CountAsync(),
            };

            response.TotalPages = (int)Math.Ceiling(response.TotalItems / (double)pageSize);
            response.Courses = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(response);
        }
        catch { return Problem(); }
    }
    

    #endregion


    #region UPDATE

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Bearer")]
   
    public async Task<IActionResult> Update(CourseEntity course)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var result = await _courseRepository.UpdateAsync(x => x.Id == course.Id, course);
                if (result != null)
                    return Ok();
            }
            catch { return Problem(); }
        }
        return BadRequest();
    }

    #endregion


    #region DELETE 

    [HttpDelete]
    [Authorize(AuthenticationSchemes = "Bearer")]
  
    public async Task<IActionResult> Delete(CourseEntity course)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var exists = await _courseRepository.ExistsAsync(x => x.Id == course.Id);
                if (exists)
                {
                    var result = await _courseRepository.DeleteAsync(x => x.Id == course.Id);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex) { return Problem(); }
        }
        return BadRequest();
    }

    #endregion

}
