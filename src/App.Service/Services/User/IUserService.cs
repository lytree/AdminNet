using App.Core.Dto;
using App.Repository.Domain;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace App.Service.Services;

/// <summary>
/// 用户接口
/// </summary>
public interface IUserService
{
    Task<UserGetOutput> GetAsync(long id);

    Task<PageOutput<UserGetPageOutput>> GetPageAsync(PageInput<UserGetPageDto> input);

    Task<AuthLoginOutput> GetLoginUserAsync(long id);

    Task<DataPermissionOutput> GetDataPermissionAsync(string? apiPath);

    Task<long> AddAsync(UserAddInput input);

    Task<long> AddMemberAsync(UserAddMemberInput input);

    Task UpdateAsync(UserUpdateInput input);

    Task DeleteAsync(long id);

    Task BatchDeleteAsync(long[] ids);

    Task SoftDeleteAsync(long id);

    Task BatchSoftDeleteAsync(long[] ids);

    Task ChangePasswordAsync(UserChangePasswordInput input);

    Task<string> ResetPasswordAsync(UserResetPasswordInput input);

    Task SetManagerAsync(UserSetManagerInput input);

    Task UpdateBasicAsync(UserUpdateBasicInput input);

    Task<UserGetBasicOutput> GetBasicAsync();

    Task<UserGetPermissionOutput> GetPermissionAsync();

    Task<string> AvatarUpload(IFormFile file, bool autoUpdate = false);

    Task<dynamic> OneClickLoginAsync(string userName);
}