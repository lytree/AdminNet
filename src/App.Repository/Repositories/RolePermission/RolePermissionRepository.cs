using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class RolePermissionRepository : AdminRepositoryBase<RolePermissionEntity>, IRolePermissionRepository
{
    public RolePermissionRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {

    }
}