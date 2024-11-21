
using Framework.Repository.Repositories;
using Server.Repository.Domain;

namespace Server.Repository.Repositories;

public class TaskExtRepository : RepositoryBase<TaskInfoExt>, ITaskExtRepository
{
    public TaskExtRepository(UnitOfWorkManagerCloud uowm) : base(uowm.GetUnitOfWorkManager(DbKeys.TaskDb))
    {
    }
}