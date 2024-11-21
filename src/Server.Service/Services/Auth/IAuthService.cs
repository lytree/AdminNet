using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using Server.Service.Services.Auth.Dto;

namespace Server.Service.Services.Auth;

/// <summary>
/// 认证授权接口
/// </summary>
public interface IAuthService
{
    string GetToken(AuthLoginOutput user);

    Task<dynamic> LoginAsync(AuthLoginInput input);

    Task<AuthGetUserInfoOutput> GetUserInfoAsync();

    Task<AuthGetPasswordEncryptKeyOutput> GetPasswordEncryptKeyAsync();

    Task<dynamic> Refresh([BindRequired] string token);

}