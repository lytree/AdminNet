using FreeScheduler;

using Framework.Repository.Repositories;

namespace App.Repository.Repositories;

public class TaskRepository : RepositoryBase<TaskInfo>, ITaskRepository
{
    public TaskRepository(UnitOfWorkManagerCloud uowm) : base(uowm.GetUnitOfWorkManager(DbKeys.TaskDb))
    {
    }
}