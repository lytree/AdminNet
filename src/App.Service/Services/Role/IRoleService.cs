using App.Core.Dto;
using App.Repository.Domain;
using System.Collections.Generic;


namespace App.Service.Services;

/// <summary>
/// 角色接口
/// </summary>
public interface IRoleService
{
    Task<RoleGetOutput> GetAsync(long id);

    Task<List<RoleGetListOutput>> GetListAsync(RoleGetListInput input);

    Task<PageOutput<RoleGetPageOutput>> GetPageAsync(PageInput<RoleGetPageDto> input);

    Task<long> AddAsync(RoleAddInput input);

    Task AddRoleUserAsync(RoleAddRoleUserListInput input);

    Task RemoveRoleUserAsync(RoleAddRoleUserListInput input);

    Task UpdateAsync(RoleUpdateInput input);

    Task DeleteAsync(long id);

    Task BatchDeleteAsync(long[] ids);

    Task SoftDeleteAsync(long id);

    Task BatchSoftDeleteAsync(long[] ids);

    Task SetDataScopeAsync(RoleSetDataScopeInput input);
}