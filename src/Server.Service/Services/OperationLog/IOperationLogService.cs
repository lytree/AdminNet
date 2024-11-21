using System.Threading.Tasks;
using Server.Service.Core.Dto;
using Server.Service.Services.OperationLog.Dto;

namespace Server.Service.Services.OperationLog;

/// <summary>
/// 操作日志接口
/// </summary>
public interface IOperationLogService
{
    Task<PageOutput<OperationLogGetPageOutput>> GetPageAsync(PageInput<OperationLogGetPageInput> input);

    Task<long> AddAsync(OperationLogAddInput input);
}