using System.Collections.Generic;

namespace Server.Service.Services.Auth.Dto;

public class AuthGetUserPermissionsOutput
{
    /// <summary>
    /// 用户权限列表
    /// </summary>
    public List<string> Permissions { get; set; }
}