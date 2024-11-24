

using App.Core.Dto;
using App.Repository.Domain;

namespace App.Service.Services;

/// <summary>
/// api接口
/// </summary>
public interface IApiService
{
    Task<ApiGetOutput> GetAsync(long id);

    Task<List<ApiGetListOutput>> GetListAsync(string key);

    Task<PageOutput<ApiEntity>> GetPageAsync(PageInput<ApiGetPageDto> input);

    Task<long> AddAsync(ApiAddInput input);

    Task UpdateAsync(ApiUpdateInput input);

    Task DeleteAsync(long id);

    Task BatchDeleteAsync(long[] ids);

    Task SoftDeleteAsync(long id);

    Task BatchSoftDeleteAsync(long[] ids);

    Task SyncAsync(ApiSyncInput input);
}