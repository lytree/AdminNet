using System.ComponentModel.DataAnnotations;
using Server.Service.Core.Validators;

namespace Server.Service.Services.Permission.Dto;

public class PermissionUpdateGroupInput : PermissionAddGroupInput
{
    /// <summary>
    /// 权限Id
    /// </summary>
    [Required]
    [ValidateRequired("请选择分组")]
    public long Id { get; set; }
}