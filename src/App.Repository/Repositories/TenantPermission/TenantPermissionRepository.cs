using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class TenantPermissionRepository : AdminRepositoryBase<TenantPermissionEntity>, ITenantPermissionRepository
{
    public TenantPermissionRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {

    }
}