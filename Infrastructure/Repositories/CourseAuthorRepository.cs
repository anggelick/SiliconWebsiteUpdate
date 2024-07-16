using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CourseAuthorRepository : Repo<CourseAuthorEntity>
{
    public CourseAuthorRepository(DataContext context) : base(context)
    {
    }
}
