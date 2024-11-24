using FreeScheduler;
using App.Service.Core.Dto;
using App.Service.Domain.Task.Dto;

namespace App.Service.Services;

/// <summary>
/// 任务日志接口
/// </summary>
public interface ITaskLogService
{
    PageOutput<TaskLog> GetPage(PageInput<TaskLogGetPageDto> input);

    void Add(TaskLog input);
}