﻿namespace App.Service.Services;

/// <summary>
/// 用户分页查询条件
/// </summary>
public class UserGetPageDto
{
    /// <summary>
    /// 部门Id
    /// </summary>
    public long? OrgId { get; set; }
}
