using Server.Repository.Domain;
using Server.Repository.Repositories;


namespace Server.Repository.Repositories;

public class OperationLogRepository : AdminRepositoryBase<OperationLogEntity>, IOperationLogRepository
{
    public OperationLogRepository(UnitOfWorkManagerCloud uowm) : base(uowm)
    {
    }
}