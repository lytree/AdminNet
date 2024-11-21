using Server.Service.Core.Dto;
using System.Threading.Tasks;
using Server.Service.Services.LoginLog.Dto;

namespace Server.Service.Services.LoginLog;

/// <summary>
/// 登录日志接口
/// </summary>
public interface ILoginLogService
{
    Task<PageOutput<LoginLogGetPageOutput>> GetPageAsync(PageInput<LoginLogGetPageInput> input);

    Task<long> AddAsync(LoginLogAddInput input);
}