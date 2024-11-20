using FreeScheduler;


using Framework.Repository.Repositories;

namespace Server.Repository.Repositories;

public class TaskLogRepository : RepositoryBase<TaskLog>, ITaskLogRepository
{
    public TaskLogRepository(UnitOfWorkManagerCloud uowm) : base(DbKeys.TaskDb, uowm)
    {
    }
}