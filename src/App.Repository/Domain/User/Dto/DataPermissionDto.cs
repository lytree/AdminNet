﻿
using System.Collections.Generic;

namespace App.Repository.Domain;

public class DataPermissionDto
{
    /// <summary>
    /// 部门Id
    /// </summary>
    public long OrgId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string OrgName { get; set; }
    /// <summary>
    /// 部门列表
    /// </summary>
    public List<long> OrgIds { get; set; }

    /// <summary>
    /// 数据范围
    /// </summary>
    public DataScope DataScope { get; set; } = DataScope.Self;
}