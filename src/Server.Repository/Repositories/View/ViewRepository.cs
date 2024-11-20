using Server.Repository.Domain;
using Server.Repository.Repositories;


namespace Server.Repository.Repositories;

public class ViewRepository : AdminRepositoryBase<ViewEntity>, IViewRepository
{
    public ViewRepository(UnitOfWorkManagerCloud muowm) : base(muowm)
    {
    }
}