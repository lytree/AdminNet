using App.Repository.Domain;
using App.Repository.Repositories;


namespace App.Repository.Repositories;

public class OperationLogRepository : AdminRepositoryBase<OperationLogEntity>, IOperationLogRepository
{
    public OperationLogRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}