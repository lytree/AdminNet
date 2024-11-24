using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class RoleOrgRepository : AdminRepositoryBase<RoleOrgEntity>, IRoleOrgRepository
{
    public RoleOrgRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {

    }
}