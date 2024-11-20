using Server.Repository.Domain;
using Server.Repository.Repositories;


namespace Server.Repository.Repositories;

public class TenantPermissionRepository : AdminRepositoryBase<TenantPermissionEntity>, ITenantPermissionRepository
{
    public TenantPermissionRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {

    }
}