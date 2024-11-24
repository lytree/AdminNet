
using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class PermissionRepository : AdminRepositoryBase<PermissionEntity>, IPermissionRepository
{
    public PermissionRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}