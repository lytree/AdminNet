using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class UserStaffRepository : AdminRepositoryBase<UserStaffEntity>, IUserStaffRepository
{
    public UserStaffRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {

    }
}