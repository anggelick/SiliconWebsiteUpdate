using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CourseCategoryRepository : Repo<CourseCategoryEntity>
{
    public CourseCategoryRepository(DataContext context) : base(context)
    {
    }
}
