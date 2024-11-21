using System.ComponentModel.DataAnnotations;
using Server.Service.Core.Validators;

namespace Server.Service.Services.Role.Dto;

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