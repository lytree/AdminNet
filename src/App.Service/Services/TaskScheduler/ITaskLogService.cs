using App.Core.Dto;
using FreeScheduler;


namespace App.Service.Services;

/// <summary>
/// 任务日志接口
/// </summary>
public interface ITaskLogService
{
    PageOutput<TaskLog> GetPage(PageInput<TaskLogGetPageDto> input);

    void Add(TaskLog input);
}