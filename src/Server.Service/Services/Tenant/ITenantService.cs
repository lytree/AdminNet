using System.Threading.Tasks;
using Server.Service.Core.Dto;
using Server.Service.Domain.Tenant.Dto;
using Server.Service.Services.Tenant.Dto;

namespace Server.Service.Services.Tenant;

/// <summary>
/// 租户接口
/// </summary>
public interface ITenantService
{
    Task<TenantGetOutput> GetAsync(long id);

    Task<PageOutput<TenantListOutput>> GetPageAsync(PageInput<TenantGetPageDto> input);

    Task<long> AddAsync(TenantAddInput input);

    Task<long> RegAsync(TenantRegInput input);

    Task UpdateAsync(TenantUpdateInput input);

    Task DeleteAsync(long id);

    Task SoftDeleteAsync(long id);

    Task BatchSoftDeleteAsync(long[] ids);
}