using Server.Repository.Domain;
using Server.Repository.Repositories;


namespace Server.Repository.Repositories.UserRole;

public class UserRoleRepository : AdminRepositoryBase<UserRoleEntity>, IUserRoleRepository
{
    public UserRoleRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {

    }
}