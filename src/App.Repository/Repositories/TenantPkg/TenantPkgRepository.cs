using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class TenantPkgRepository : AdminRepositoryBase<TenantPkgEntity>, ITenantPkgRepository
{
    public TenantPkgRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {

    }
}