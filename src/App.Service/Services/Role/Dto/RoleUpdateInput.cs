using System.ComponentModel.DataAnnotations;
using App.Service.Core.Validators;

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
    [ValidateRequired("请选择角色")]
    public long Id { get; set; }
}