using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class TenantRepository : AdminRepositoryBase<TenantEntity>, ITenantRepository
{
    public TenantRepository(UnitOfWorkManagerCloud muowm) : base(muowm)
    {
    }
}