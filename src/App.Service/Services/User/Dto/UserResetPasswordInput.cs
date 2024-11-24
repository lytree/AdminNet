using App.Service.Core.Entities;

namespace App.Service.Services;

/// <summary>
/// 重置密码
/// </summary>
public class UserResetPasswordInput : Entity
{
    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }
}