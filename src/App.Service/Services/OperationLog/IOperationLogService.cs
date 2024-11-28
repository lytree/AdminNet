using App.Core.Dto;
using System.Threading.Tasks;

namespace App.Service.Services;

/// <summary>
/// 操作日志接口
/// </summary>
public interface IOperationLogService
{
    Task<PageOutput<OperationLogGetPageOutput>> GetPageAsync(PageInput<OperationLogGetPageInput> input);

    Task<long> AddAsync(OperationLogAddInput input);
}