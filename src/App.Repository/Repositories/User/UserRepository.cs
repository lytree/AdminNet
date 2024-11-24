using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class UserRepository : AdminRepositoryBase<UserEntity>, IUserRepository
{
    public UserRepository(UnitOfWorkManagerCloud muowm) : base(muowm)
    {

    }
}