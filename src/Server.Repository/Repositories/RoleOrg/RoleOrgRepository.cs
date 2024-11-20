using Server.Repository.Domain;
using Server.Repository.Repositories;


namespace Server.Repository.Repositories;

public class RoleOrgRepository : AdminRepositoryBase<RoleOrgEntity>, IRoleOrgRepository
{
    public RoleOrgRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {

    }
}