

using App.Repository.Domain;

namespace App.Repository.Repositories.Api;

public class ApiRepository : AdminRepositoryBase<ApiEntity>, IApiRepository
{
    public ApiRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}