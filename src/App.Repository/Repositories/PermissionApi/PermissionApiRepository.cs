using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class PermissionApiRepository : AdminRepositoryBase<PermissionApiEntity>, IPermissionApiRepository
{
    public PermissionApiRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {

    }
}