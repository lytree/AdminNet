using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class ViewRepository : AdminRepositoryBase<ViewEntity>, IViewRepository
{
    public ViewRepository(UnitOfWorkManagerCloud muowm) : base(muowm)
    {
    }
}