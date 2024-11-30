using System.ComponentModel.DataAnnotations;


namespace App.Service.Services;

/// <summary>
/// 修改
/// </summary>
public partial class RoleUpdateInput : RoleAddInput
{
    /// <summary>
    /// 角色Id
    /// </summary>
    [Required]
    public long Id { get; set; }
}