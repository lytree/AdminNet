
using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class LoginLogRepository : AdminRepositoryBase<LoginLogEntity>, ILoginLogRepository
{
    public LoginLogRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}