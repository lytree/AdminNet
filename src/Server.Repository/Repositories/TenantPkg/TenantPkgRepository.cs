using Server.Repository.Domain;
using Server.Repository.Repositories;


namespace Server.Repository.Repositories;

public class TenantPkgRepository : AdminRepositoryBase<TenantPkgEntity>, ITenantPkgRepository
{
    public TenantPkgRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {

    }
}