using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class SubscriberRepository: Repo<SubscriberEntity>
{
    public SubscriberRepository(DataContext context) : base(context)
    {
    }
}
