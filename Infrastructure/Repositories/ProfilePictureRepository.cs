using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class ProfilePictureRepository : Repo<ProfilePictureEntity>
{
    public ProfilePictureRepository(DataContext context) : base(context)
    {
    }
}
