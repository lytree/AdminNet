using System.Threading.Tasks;
using App.Service.Core.Dto;
using App.Service.Services.OperationLog.Dto;

namespace App.Service.Services;

/// <summary>
/// 操作日志接口
/// </summary>
public interface IOperationLogService
{
    Task<PageOutput<OperationLogGetPageOutput>> GetPageAsync(PageInput<OperationLogGetPageInput> input);

    Task<long> AddAsync(OperationLogAddInput input);
}