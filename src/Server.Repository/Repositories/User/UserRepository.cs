using Server.Repository.Domain;
using Server.Repository.Repositories;


namespace Server.Repository.Repositories;

public class UserRepository : AdminRepositoryBase<UserEntity>, IUserRepository
{
    public UserRepository(UnitOfWorkManagerCloud muowm) : base(muowm)
    {

    }
}