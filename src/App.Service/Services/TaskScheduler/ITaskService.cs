using System.Threading.Tasks;
using App.Service.Core.Dto;
using App.Service.Domain.Task.Dto;
using App.Service.Services.TaskScheduler.Dto;

namespace App.Service.Services;

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