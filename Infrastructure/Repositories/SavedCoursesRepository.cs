using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class SavedCoursesRepository: Repo<SavedCoursesEntity>
{
    public SavedCoursesRepository(DataContext context) : base(context)
    {
    }
}
