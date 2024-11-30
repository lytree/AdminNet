using System.ComponentModel.DataAnnotations;
using App.Repository.Domain;

namespace App.Service.Services;

/// <summary>
/// 设置数据范围
/// </summary>
public class RoleSetDataScopeInput
{
    /// <summary>
    /// 角色Id
    /// </summary>
    [Required]
    public long RoleId { get; set; }

    /// <summary>
    /// 数据范围
    /// </summary>
    public DataScope DataScope { get; set; }

    /// <summary>
    /// 指定部门
    /// </summary>
    public long[] OrgIds { get; set; }
}