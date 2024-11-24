using System.Collections.Generic;

namespace App.Service.Services;

public class AuthGetUserPermissionsOutput
{
    /// <summary>
    /// 用户权限列表
    /// </summary>
    public List<string> Permissions { get; set; }
}