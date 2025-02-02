﻿
using FreeScheduler;
using Mapster;
using System.Collections.Generic;

using System;
using App.Repository.Repositories;
using App.Core.Dto;


namespace App.Service.Services;

/// <summary>
/// 任务日志服务
/// </summary>


public class TaskLogService : BaseService, ITaskLogService
{
    private readonly Scheduler _scheduler;
    
    private readonly Lazy<ITaskLogRepository> _taskLogRep;

    public TaskLogService(Scheduler scheduler,
        ,
        Lazy<ITaskLogRepository> taskLogRep)
    {
        _scheduler = scheduler;
        
        _taskLogRep = taskLogRep;
    }

    /// <summary>
    /// 查询分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>

    public PageOutput<TaskLog> GetPage(PageInput<TaskLogGetPageDto> input)
    {
        if (!(input.Filter != null && input.Filter.TaskId.NotNull()))
        {
            throw ResultOutput.Exception("请选择任务"]);
        }

        var result = Datafeed.GetLogs(_scheduler, input.Filter.TaskId, input.PageSize, input.CurrentPage);

        var data = new PageOutput<TaskLog>()
        {
            List = result.Logs.Adapt<List<TaskLog>>(),
            Total = result.Total
        };

        return data;
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>

    public void Add(TaskLog input)
    {
        _taskLogRep.Value.InsertAsync(input);
    }
}