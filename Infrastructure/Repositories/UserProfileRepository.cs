using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class UserProfileRepository : Repo<UserProfileEntity>
{
    private readonly DataContext _context;

    public UserProfileRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<List<UserProfileEntity>> GetAllAsync()
    {
        try
        {
            List<UserProfileEntity> result = await _context.UserProfiles
                .Include(x => x.Address)
                .Include(x => x.ProfilePicture)               
                .ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null!;
        }
    }

    public override async Task<UserProfileEntity> GetOneAsync(Expression<Func<UserProfileEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.UserProfiles
                .Include(x => x.Address)
                .Include(x => x.ProfilePicture)
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
