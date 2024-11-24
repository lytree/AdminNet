

using App.Core.Dto;

namespace App.Service.Services;

/// <summary>
/// 登录日志接口
/// </summary>
public interface ILoginLogService
{
    Task<PageOutput<LoginLogGetPageOutput>> GetPageAsync(PageInput<LoginLogGetPageInput> input);

    Task<long> AddAsync(LoginLogAddInput input);
}