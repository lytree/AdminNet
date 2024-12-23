﻿using FreeScheduler;


using Framework.Repository.Repositories;

namespace App.Repository.Repositories;

public class TaskLogRepository : RepositoryBase<TaskLog>, ITaskLogRepository
{
    public TaskLogRepository(UnitOfWorkManagerCloud uowm) : base(uowm.GetUnitOfWorkManager(DbKeys.TaskDb))
    {
    }
}