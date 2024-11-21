using FreeScheduler;
using Server.Service.Core.Dto;
using Server.Service.Domain.Task.Dto;

namespace Server.Service.Services.TaskScheduler;

/// <summary>
/// 任务日志接口
/// </summary>
public interface ITaskLogService
{
    PageOutput<TaskLog> GetPage(PageInput<TaskLogGetPageDto> input);

    void Add(TaskLog input);
}