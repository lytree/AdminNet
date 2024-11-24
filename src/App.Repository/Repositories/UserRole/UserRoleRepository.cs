using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories.UserRole;

public class UserRoleRepository : AdminRepositoryBase<UserRoleEntity>, IUserRoleRepository
{
    public UserRoleRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {

    }
}