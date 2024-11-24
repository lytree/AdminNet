using Framework.Repository.Entities;
using FreeSql.DataAnnotations;

namespace App.Repository.Domain;

/// <summary>
/// 租户套餐
/// </summary>
[Table(Name = "ad_tenant_pkg")]
[Index("idx_{tablename}_01", nameof(TenantId) + "," + nameof(PkgId), true)]
public class TenantPkgEntity : EntityAdd
{
    /// <summary>
    /// 租户Id
    /// </summary>
    public long TenantId { get; set; }

    public TenantEntity Tenant { get; set; }

    /// <summary>
    /// 套餐Id
    /// </summary>
    public long PkgId { get; set; }

    public PkgEntity Pkg { get; set; }
}