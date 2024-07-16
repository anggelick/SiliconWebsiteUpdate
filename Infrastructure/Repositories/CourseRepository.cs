using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class CourseRepository : Repo<CourseEntity>
{
    private readonly DataContext _context;
    public CourseRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<List<CourseEntity>> GetAllAsync()
    {
        try
        {
            List<CourseEntity> result = await _context.Courses
                .Include(x => x.Image)
                .Include(x => x.CourseAuthor)
                .Include(x => x.CourseAuthor.Image)
                .Include(x => x.CourseCategory)
                .ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null!;
        }
    }

    public IQueryable<CourseEntity> GetAll()
    {
        try
        {
            IQueryable<CourseEntity> result = _context.Courses
                .Include(x => x.Image)
                .Include(x => x.CourseAuthor)
                .Include(x => x.CourseAuthor.Image)
                .Include(x => x.CourseCategory);
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null!;
        }
    }

    public override async Task<CourseEntity> GetOneAsync(Expression<Func<CourseEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Courses
                .Include(x => x.Image)
                .Include(x => x.CourseAuthor)
                .Include(x => x.CourseAuthor.Image)
                .FirstOrDefaultAsync(predicate);
            if (result == null)
            {
                Console.WriteLine($"Error: 404 - Entity not found");
                return null!;
            }

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null!;
        }
    }
}