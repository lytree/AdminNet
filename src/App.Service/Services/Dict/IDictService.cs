using App.Core.Dto;

namespace App.Service.Services;

/// <summary>
/// 数据字典接口
/// </summary>
public partial interface IDictService
{
    Task<DictGetOutput> GetAsync(long id);

    Task<PageOutput<DictGetPageOutput>> GetPageAsync(PageInput<DictGetPageInput> input);

    Task<long> AddAsync(DictAddInput input);

    Task UpdateAsync(DictUpdateInput input);

    Task DeleteAsync(long id);

    Task BatchDeleteAsync(long[] ids);

    Task SoftDeleteAsync(long id);

    Task BatchSoftDeleteAsync(long[] ids);
}