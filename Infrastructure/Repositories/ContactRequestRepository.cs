using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class ContactRequestRepository : Repo<ContactRequestEntity>
{
    public ContactRequestRepository(DataContext context) : base(context)
    {
    }
}
