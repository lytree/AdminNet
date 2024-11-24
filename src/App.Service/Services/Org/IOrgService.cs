

namespace App.Service.Services;

public partial interface IOrgService
{
    Task<OrgGetOutput> GetAsync(long id);

    Task<List<OrgListOutput>> GetListAsync(string key);

    Task<long> AddAsync(OrgAddInput input);

    Task UpdateAsync(OrgUpdateInput input);

    Task DeleteAsync(long id);

    Task SoftDeleteAsync(long id);
}