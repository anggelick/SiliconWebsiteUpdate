using Infrastructure.Contexts;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class Repo<TEntity> where TEntity : class
{
    private readonly DataContext _context;

    public Repo(DataContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        try 
        { 
            var result = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null!;
        }
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        try
        {
            List<TEntity> result = await _context.Set<TEntity>().ToListAsync();
            return result;
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null!;
        }
    }

    public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (result != null)
            {
                return result!; 
            }

            Console.WriteLine($"Error 404 - Entity not found");
            return null!;
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null!;
        }
    }

    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (result != null)
            {
                _context.Entry(result).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            Console.WriteLine($"404 - Error found");
            return null!;
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error {ex.Message}");
            return null!;
        }
    }

    public virtual async Task<TEntity> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (result != null)
            {
                _context.Set<TEntity>().Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            Console.WriteLine($"Error : 404 - Entity not found");
            return null!;

        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null!;
        }
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Set<TEntity>().AnyAsync(predicate);
            if (result == true)
            {
                return true;
            }
            return false;
        }   
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
}
