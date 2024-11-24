﻿using Framework.Repository.Entities;
using FreeSql.DataAnnotations;
using Framework.Repository.Attributes;

namespace App.Repository.Domain;

/// <summary>
/// 租户权限
/// </summary>
[Table(Name = "ad_tenant_permission")]
[Index("idx_{tablename}_01", nameof(TenantId) + "," + nameof(PermissionId), true)]
public class TenantPermissionEntity : EntityAdd
{
    /// <summary>
    /// 租户Id
    /// </summary>
	public long TenantId { get; set; }

    /// <summary>
    /// 权限Id
    /// </summary>
	public long PermissionId { get; set; }

    /// <summary>
    /// 租户
    /// </summary>
    [NotGen]
    public TenantEntity Tenant { get; set; }

    /// <summary>
    /// 权限
    /// </summary>
    [NotGen]
    public PermissionEntity Permission { get; set; }
}