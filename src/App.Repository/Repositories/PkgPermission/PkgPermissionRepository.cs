using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class PkgPermissionRepository : AdminRepositoryBase<PkgPermissionEntity>, IPkgPermissionRepository
{
    public PkgPermissionRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {

    }
}