
using Framework.Repository.Repositories;
using App.Repository.Domain;

namespace App.Repository.Repositories;

public class TaskExtRepository : RepositoryBase<TaskInfoExt>, ITaskExtRepository
{
    public TaskExtRepository(UnitOfWorkManagerCloud uowm) : base(uowm.GetUnitOfWorkManager(DbKeys.TaskDb))
    {
    }
}