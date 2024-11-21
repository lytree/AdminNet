using System.Threading.Tasks;
using Server.Service.Core.Dto;
using Server.Service.Domain.Task.Dto;
using Server.Service.Services.TaskScheduler.Dto;

namespace Server.Service.Services.TaskScheduler;

/// <summary>
/// 任务接口
/// </summary>
public interface ITaskService
{
    Task<TaskGetOutput> GetAsync(string id);

    Task<PageOutput<TaskListOutput>> GetPage(PageInput<TaskGetPageInput> input);

    Task<string> Add(TaskAddInput input);

    Task UpdateAsync(TaskUpdateInput input);

    void Pause(string id);

    void Resume(string id);

    void Run(string id);

    Task Delete(string id);
}