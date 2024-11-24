

using App.Core.Configs;
using App.Core.Dto;
using App.Core.Records;
using App.Repository.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace App.Service.Services;

/// <summary>
/// 操作日志服务
/// </summary>


public class OperationLogService : BaseService, IOperationLogService
{
    private readonly IHttpContextAccessor _context;
    private readonly IOperationLogRepository _operationLogRep;
    private readonly IOptions<AppConfig> _appConfig;

    public OperationLogService(
        IHttpContextAccessor context,
        IOperationLogRepository operationLogRep,
        IOptions<AppConfig> appConfig
    )
    {
        _context = context;
        _operationLogRep = operationLogRep;
        _appConfig = appConfig;
    }

    /// <summary>
    /// 获得IP地址
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    private LocationInfo GetIpLocationInfo(string ip)
    {
        var locationInfo = new LocationInfo();
        if (_appConfig.Value.IP2Region.Enable)
        {
            var region = AppInfo.GetRequiredService<ISearcher>().Search(ip);
            locationInfo = LocationInfo.Parse(region);
        }

        return locationInfo;
    }

    /// <summary>
    /// 查询分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>

    public async Task<PageOutput<OperationLogGetPageOutput>> GetPageAsync(PageInput<OperationLogGetPageInput> input)
    {
        var select = _operationLogRep.Select.DisableGlobalFilter()
        .WhereDynamicFilter(input.DynamicFilter);

        if (input.Filter != null)
        {
            var addStartTime = input.Filter.AddStartTime;
            var addEndTime = input.Filter.AddEndTime;
            select = select
            .WhereIf(input.Filter.CreatedUserName.NotNull(), a => a.CreatedUserName.Contains(input.Filter.CreatedUserName))
            .WhereIf(input.Filter.Status.HasValue, a => a.Status == input.Filter.Status)
            .WhereIf(input.Filter.Api.NotNull(), a => a.ApiPath.Contains(input.Filter.Api) || a.ApiLabel.Contains(input.Filter.Api))
            .WhereIf(input.Filter.IP.NotNull(), a => a.IP.Contains(input.Filter.IP))
            .WhereIf(addStartTime.HasValue && !addEndTime.HasValue, a => a.CreatedTime >= addStartTime)
            .WhereIf(addEndTime.HasValue && !addStartTime.HasValue, a => a.CreatedTime < addEndTime.Value.AddDays(1))
            .WhereIf(addStartTime.HasValue && addEndTime.HasValue, a => a.CreatedTime.Value.BetweenEnd(addStartTime.Value, addEndTime.Value.AddDays(1)))
            ;
        }

        var list = await select
        .Count(out var total)
        .OrderByDescending(true, c => c.Id)
        .Page(input.CurrentPage, input.PageSize)
        .ToListAsync<OperationLogGetPageOutput>();

        var data = new PageOutput<OperationLogGetPageOutput>()
        {
            List = list,
            Total = total
        };

        return data;
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<long> AddAsync(OperationLogAddInput input)
    {
        string ua = _context.HttpContext.Request.Headers["User-Agent"];
        if (ua.NotNull())
        {
            var client = UAParser.Parser.GetDefault().Parse(ua);
            var device = client.Device.Family;
            device = device.ToLower() == "other" ? "" : device;
            input.Browser = client.UA.Family;
            input.Os = client.OS.Family;
            input.Device = device;
            input.BrowserInfo = ua;
        }
        input.Name = User.Name;

        var entity = Mapper.Map<OperationLogEntity>(input);

        if (entity.IP.IsNull())
        {
            entity.IP = IPHelper.GetIP(_context?.HttpContext?.Request);
            var locationInfo = GetIpLocationInfo(entity.IP);
            entity.Country = locationInfo?.Country;
            entity.Province = locationInfo?.Province;
            entity.City = locationInfo?.City;
            entity.Isp = locationInfo?.Isp;
        }

        await _operationLogRep.InsertAsync(entity);

        return entity.Id;
    }
}